

using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Clients.CreateClient;

public record CreateClientCommand(
     string Name,
     string LastName,
     string Dni,
     string Phone
): ICommand<Guid>;  