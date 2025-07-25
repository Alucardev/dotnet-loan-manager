namespace LoanManager.Api.Controllers.Clientes;

public sealed class CreateClientRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Dni { get; set; }
    public string Phone { get; set; }
}
