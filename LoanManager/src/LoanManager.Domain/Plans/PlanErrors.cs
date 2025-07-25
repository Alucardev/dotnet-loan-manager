using   LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Plans;


public static class PlanErrors
{
    public static readonly Error NotFound = new(
        "Plan.NotFound",
        "El id del Plan es invalido o no existe."
    );
}