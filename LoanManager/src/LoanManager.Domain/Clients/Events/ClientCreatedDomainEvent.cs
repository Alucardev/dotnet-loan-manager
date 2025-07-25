using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Clients.Events;
public sealed record ClientCreatedDomainEvent(ClientId ClienteId): IDomainEvent;