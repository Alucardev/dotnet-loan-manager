namespace LoanManager.Domain.Plans;

public record PlanId(Guid Value)
{
    public static PlanId New() => new(Guid.NewGuid());
}