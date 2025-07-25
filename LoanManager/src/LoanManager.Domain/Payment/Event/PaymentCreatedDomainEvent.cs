using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Payments.Event;

public sealed record PaymentCreatedDomainEvent(PaymentId PaymentId) : IDomainEvent;