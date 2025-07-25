using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Installments.Event;

public sealed record InstallmentCreatedDomainEvent(InstallmentId InstallmentId) : IDomainEvent;