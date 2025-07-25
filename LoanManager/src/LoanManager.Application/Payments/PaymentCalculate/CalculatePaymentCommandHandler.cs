using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Payments;
using LoanManager.Domain.Plans;


namespace LoanManager.Application.Payments.CalculatePayment;

public class CalcularPagosCommandHandler:ICommandHandler<CalculatePaymentCommand,CalculatePaymentResponse>
{
    private readonly IPlanRepository _planRepository;
    private readonly IInstallmentRespository _installmentRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly PaymentSurchargeCalculator _paymentSurchargeCalculatorService;


    public CalcularPagosCommandHandler(
        IPlanRepository planRepository,
        IInstallmentRespository installmentRepository, 
        PaymentSurchargeCalculator pagoRecargoCalculatorService,
        ILoanRepository loanRepository
        )
    {

        _planRepository = planRepository;
        _installmentRepository = installmentRepository;
        _loanRepository = loanRepository;
        _paymentSurchargeCalculatorService = pagoRecargoCalculatorService;
    }

    public async Task<Result<CalculatePaymentResponse>> Handle(CalculatePaymentCommand request, CancellationToken cancellationToken)
    {

    
        var installment = await _installmentRepository.GetByIdAsync(new InstallmentId(request.InstallmentId));
        var loan = await _loanRepository.GetByIdAsync(new LoanId(installment!.LoanId.Value));
        var plan = await _planRepository.GetByIdAsync(new PlanId(loan!.PlanId.Value));
        var surcharge = _paymentSurchargeCalculatorService.Calculate(plan!, installment!, DateTime.UtcNow);
       
        if(installment is null) return Result.Failure<CalculatePaymentResponse>(InstallmentErrors.NotFound);
        if(loan is null) return Result.Failure<CalculatePaymentResponse>(LoanErrors.NotFound);
        if(plan is null) return Result.Failure<CalculatePaymentResponse>(PlanErrors.NotFound);

        var pay = new CalculatePaymentResponse
        {
            Total = installment.Amount!.Total + surcharge,
            Surcharge = surcharge
        };

        return Result.Success(pay);

    }
}   
