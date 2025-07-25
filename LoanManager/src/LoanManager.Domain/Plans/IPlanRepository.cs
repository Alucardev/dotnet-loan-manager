
using LoanManager.Domain.Plans;

namespace LoanManager.Domain.Plans;

public interface IPlanRepository {
    Task<Plan?> GetByIdAsync(PlanId id, CancellationToken cancellationToken = default);
    void  Add(Plan plan);
    void Delete(Plan plan);
}
