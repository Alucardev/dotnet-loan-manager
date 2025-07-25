using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Users;

public sealed record UserCreatedDomainEvent(UserId UserId ): IDomainEvent;