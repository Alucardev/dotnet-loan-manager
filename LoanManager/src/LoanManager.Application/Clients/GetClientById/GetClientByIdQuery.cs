using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Clients.GetClienteById;
public sealed record GetClientByIdQuery(Guid Id) : IQuery<GetClientByIdResponse>;