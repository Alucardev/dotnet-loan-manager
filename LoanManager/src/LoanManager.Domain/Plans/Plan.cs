using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Plans.Events;

namespace LoanManager.Domain.Plans;

public sealed class Plan : Entity<PlanId>
{
    private Plan(){}
    private Plan(PlanId id, TotalInstallments totalInstallments, Interest interest, Penalty penalty, Frequency frequency)  : base(id)
    {
        TotalInstallments = totalInstallments;
        Interest = interest;
        Penalty = penalty;
        Frequency = frequency;
    }
    public TotalInstallments TotalInstallments { get; private set; }
    public Interest Interest { get; private set; }
    public Penalty Penalty { get; private set; }
    public Frequency Frequency{ get; private set; }

    public static Plan Create(TotalInstallments totalInstallments, Interest interest, Penalty penalty, Frequency frequency)
    {
        var plan = new Plan(PlanId.New(),totalInstallments, interest, penalty, frequency);
        plan.RaiseDomainEvent(new PlanCreatedDomainEvent(plan.Id));
        return plan;
    }
}