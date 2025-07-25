using MediatR;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Payments;
using LoanManager.Domain.Plans;
using LoanManager.Domain.Shared;
using LoanManager.Application.Loans.CheckLoanCompleted;

namespace LoanManager.Application.Payments.CraeatePayment;

    public class CrearPagosCommandHandler : ICommandHandler<CreatePaymentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ISender _sender;
        private readonly IInstallmentRespository _installmentRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly PaymentSurchargeCalculator _paymentSurchargeCalculatorService;

        public CrearPagosCommandHandler(
            ISender sender,
            IPaymentRepository paymentRepository, 
            IUnitOfWork unitOfWork, 
            IPlanRepository planRepository,
            IInstallmentRespository installmentRepository, 
            PaymentSurchargeCalculator paymentSurchargeCalculatorService,
            ILoanRepository loanRepository
        )
        {
            _sender = sender;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
            _planRepository = planRepository;
            _installmentRepository = installmentRepository;
            _loanRepository = loanRepository;
            _paymentSurchargeCalculatorService = paymentSurchargeCalculatorService;
        }

        public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            // Obtener la cuota
            var installment = await _installmentRepository.GetByIdAsync(new InstallmentId(request.InstallmentId));
            if (installment == null) return Result.Failure<Guid>(InstallmentErrors.NotFound);  // Validación de cuota
            
            // Obtener el préstamo relacionado
            var loan = await _loanRepository.GetByIdAsync(new LoanId(installment.LoanId.Value));
            if (loan == null)return Result.Failure<Guid>(LoanErrors.NotFound);  // Validación de préstamo
            // Obtener el plan relacionado al préstamo
            var plan = await _planRepository.GetByIdAsync(new PlanId(loan.PlanId.Value));
            if (plan == null) return Result.Failure<Guid>(PlanErrors.NotFound);  // Validación de plan
            

            // Calcular el recargo
            var surcharge = _paymentSurchargeCalculatorService.Calculate(plan, installment, DateTime.UtcNow);

            // Crear el pago
            var pagoResult = Payment.Create(
                installment,
                (PaymentMethod)request.PaymentMethod,
                DateTime.UtcNow,
                new Amount(surcharge, CurrencyType.FromCodigo(installment.Amount.CurrencyType.Code!))
            );

            if (pagoResult.IsFailure)
            {
                return Result.Failure<Guid>(pagoResult.Error);  // Validación de creación del pago
            }

            // Guardar el pago en el repositorio
            _paymentRepository.Add(pagoResult.Value);   
            await _unitOfWork.SaveChangesAsync();

            var finished = await _sender.Send(new CheckLoanCompletedQuery(loan.Id.Value), cancellationToken);
            if (finished.IsFailure)
            {
                return Result.Failure<Guid>(finished.Error);
            }

            // el prestamo esta completo
            if(finished.Value) {
                
                loan.Complete();
                await _unitOfWork.SaveChangesAsync();
            }

            // Devolver el ID del pago como éxito
            return Result.Success(pagoResult.Value.Id.Value);
        }
    }

