using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Loans.CheckLoanCompleted;

public record CheckLoanCompletedQuery(
    Guid LoanId
): IQuery<bool>;


