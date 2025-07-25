using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Installments;

public static class InstallmentErrors
{
    public static readonly Error AlreadyPayed = new(
        "Cuota.AlreadyPayed",
        "Esta cuota ya fue pagada."
    );

        public static readonly Error NotFound = new(
        "Cuota.NotFound",
        "El id de la cuota no existe o es invalida."
    );
}