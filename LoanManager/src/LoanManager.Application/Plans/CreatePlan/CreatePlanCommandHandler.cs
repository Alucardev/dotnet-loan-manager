using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Plans;


namespace LoanManager.Application.Plans.CreatePlans;

public class CreatePlanCommandHandler : ICommandHandler<CreatePlanCommand, Guid>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPlanRepository _planRepository;

    public CreatePlanCommandHandler(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;   
    }
    public async Task<Result<Guid>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = Plan.Create(new TotalInstallments(request.TotalInstallments), new Interest(request.Interest), new Penalty(request.Penalty), new Frequency(request.frequency));
        _planRepository.Add(plan);
        await _unitOfWork.SaveChangesAsync();
        return plan.Id.Value;
    }
}