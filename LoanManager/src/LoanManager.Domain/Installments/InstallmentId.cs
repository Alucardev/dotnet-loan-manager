namespace LoanManager.Domain.Installments;

public record InstallmentId(Guid Value)
{
    public static InstallmentId New() => new(Guid.NewGuid());

}