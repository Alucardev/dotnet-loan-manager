using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Plans.DeletePlan;

public record DeletePlanCommand(
    Guid Id
): ICommand<Guid>;