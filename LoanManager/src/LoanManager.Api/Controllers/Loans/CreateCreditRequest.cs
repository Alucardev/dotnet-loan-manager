namespace LoanManager.Api.Controllers.Prestamos;

public sealed record CreateLoanRequest(
    DateTime EmissionDate,
    Guid ClientId,
    Guid PlanId,
    decimal Amount,
    string CurrencyType,
    string description
);