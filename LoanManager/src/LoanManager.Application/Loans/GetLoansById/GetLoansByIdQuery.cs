using LoanManager.Application.Abstractions.Messaging;
namespace LoanManager.Application.Loans.GetLoansById;

public record GetLoanByIdQuery(
    Guid Id
): IQuery<GetLoanByIdResponse>;