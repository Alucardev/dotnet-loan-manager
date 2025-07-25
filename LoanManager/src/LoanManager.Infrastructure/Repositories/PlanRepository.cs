using LoanManager.Domain.Plans;

namespace LoanManager.Infrastructure.Repositories;

internal sealed class PlanRepository: Repository<Plan, PlanId>, IPlanRepository
{
    public PlanRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }

}