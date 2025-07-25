using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Plans.CreatePlans;

public record CreatePlanCommand(
    int TotalInstallments,
    decimal Interest,
    decimal Penalty,
    int frequency

): ICommand<Guid>;