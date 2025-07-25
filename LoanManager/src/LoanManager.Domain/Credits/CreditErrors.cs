using   LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Loans;


public static class LoanErrors
{
    public static readonly Error NotFound = new(
        "Loan.NotFound",
        "El id del prestamo es invalido o no existe."
    );
}