using   LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Payments;


public static class PagosErrors
{
    public static readonly Error AlreadyPayed = new(
        "Pagos.AlreadyPayed",
        "No se puede crear un pago de una cuota ya pagada."
    );
}