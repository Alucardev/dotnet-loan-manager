using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Plans;

namespace LoanManager.Application.Plans.DeletePlan;

public class DeletePlanCommandHandler : ICommandHandler<DeletePlanCommand, Guid>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPlanRepository _planRepository;

    public DeletePlanCommandHandler(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;   
    }
    public async Task<Result<Guid>> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(new PlanId(request.Id));
        if (plan == null) return Result.Failure<Guid>(PlanErrors.NotFound);
        _planRepository.Delete(plan);
        await _unitOfWork.SaveChangesAsync();
        return plan.Id.Value;
    }
}