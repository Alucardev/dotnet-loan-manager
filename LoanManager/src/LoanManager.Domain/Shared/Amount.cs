namespace LoanManager.Domain.Shared;
public record Amount(decimal Total, CurrencyType CurrencyType)
{
    public static Amount operator +(Amount first, Amount second){
        if(first.CurrencyType != second.CurrencyType)
        {
            throw new InvalidOperationException("El tipo de moneda debe ser el mismo.");
        }
        return new Amount(first.Total + second.Total, first.CurrencyType);
    }
    public static Amount Zero() => new(0, CurrencyType.None);
    public static Amount Zero(CurrencyType currencyType) => new(0, currencyType);
    public bool IsZero() => this == Zero(CurrencyType);
}
