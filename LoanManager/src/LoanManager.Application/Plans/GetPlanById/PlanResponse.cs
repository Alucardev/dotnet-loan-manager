namespace LoanManager.Application.Plans.GetPlanById;

public sealed class PlanResponse{
    public Guid Id { get; init;}
    public int TotalInstallments { get; init;}
    public decimal Interest { get; init;}
    public decimal Penalty { get; init;}
    public int Frequency { get; init;}
}