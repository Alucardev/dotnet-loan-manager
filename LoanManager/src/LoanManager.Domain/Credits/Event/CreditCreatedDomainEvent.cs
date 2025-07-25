using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Loans.Events;
public sealed record LoanCreatedDomainEvent(LoanId LoanId) : IDomainEvent;