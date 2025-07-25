using   LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Clients;


public static class ClientErrors
{
    public static readonly Error NotFound = new(
        "Cliente.NotFound",
        "El id del cliente es invalido o no existe."
    );
}