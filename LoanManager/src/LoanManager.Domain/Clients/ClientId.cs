namespace LoanManager.Domain.Clients;

public record ClientId(Guid Value)
{
    public static ClientId New() => new(Guid.NewGuid());
}

