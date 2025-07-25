namespace LoanManager.Application.Clients.GetClienteById;

public sealed class GetClientByIdResponse
{
    public Guid Id { get; init; }
    public string Name {get; init;}
    public string LastName { get; init; }
    public string Dni { get; init; }
    public string Phone { get; init; }

}