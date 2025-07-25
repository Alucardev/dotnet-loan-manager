namespace LoanManager.Api.Controllers.Plans;


public sealed record CreatePlanRequest (
    int totalInstallments,
    decimal interest,
    decimal penalty,
    int frequency
);