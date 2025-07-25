namespace LoanManager.Domain.Payments;
// hace referencia a un nombre unico con el cual uno puede acceder a este modulo

public record PaymentId(Guid Value)
{
    //creamos una funcion que retorna un record como el arriba pero con una UUID NUEVA.
    public static PaymentId New() => new(Guid.NewGuid());
}
