using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Plans.Events;

public sealed record PlanCreatedDomainEvent(PlanId planId): IDomainEvent;