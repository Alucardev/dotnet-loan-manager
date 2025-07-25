using LoanManager.Domain.Loans;
using LoanManager.Domain.Clients;
using LoanManager.Domain.Plans;
using LoanManager.Domain.Shared;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Installments;
using LoanManager.Application.Abstractions.Messaging;


namespace LoanManager.Application.Loans.CreateLoans;

internal sealed class CreateLoanCommandHandler : ICommandHandler<CreateLoanCommand, Guid>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly InstallmentCalculatorService _installmentCalculatorService;
    private readonly IInstallmentRespository _installmentRepository;

    public CreateLoanCommandHandler(
        ILoanRepository loanRepository,
        IClientRepository clientRepository,
        IPlanRepository planRepository,
        IUnitOfWork unitOfWork,
        InstallmentCalculatorService installmentCalculatorService,
        IInstallmentRespository installmentRepository
    )
    {
        _loanRepository = loanRepository;
        _clientRepository = clientRepository;
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
        _installmentCalculatorService = installmentCalculatorService;
        _installmentRepository = installmentRepository;
    }

    public async Task<Result<Guid>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(new ClientId(request.ClientId), cancellationToken);
        if (client is null)
        {
            return Result.Failure<Guid>(ClientErrors.NotFound);
        }

        var plan = await _planRepository.GetByIdAsync(new PlanId(request.PlanId), cancellationToken);
        if (plan is null)
        {
            return Result.Failure<Guid>(PlanErrors.NotFound);
        }

        var loan = Loan.Create(
            request.EmissionDate,
            client!,
            new Amount(request.Amount, CurrencyType.FromCodigo(request.CurrencyType)),
            plan!,
            new Description(request.Description)
        );


        _loanRepository.Add(loan);

    
        var installments = _installmentCalculatorService.Calculate(plan!, loan, request.EmissionDate);
        foreach (var installment in installments){
            _installmentRepository.Add(installment);
        }

        await _unitOfWork.SaveChangesAsync();
        return loan.Id.Value;
    }
}