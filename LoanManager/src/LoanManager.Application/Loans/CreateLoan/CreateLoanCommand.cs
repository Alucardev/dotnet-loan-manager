using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Loans.CreateLoans;

public record CreateLoanCommand(
    DateTime EmissionDate,
    Guid ClientId,
    Guid PlanId,
    decimal Amount,
    string CurrencyType,
    string Description
): ICommand<Guid>;