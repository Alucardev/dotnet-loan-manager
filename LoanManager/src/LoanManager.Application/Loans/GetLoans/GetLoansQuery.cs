using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Shared;

namespace LoanManager.Application.Loans.GetLoans;

public sealed record GetLoansQuery : PaginationParams, IQuery<PagedDapperResults<GetLoansResponse>>
{
    public string? Search {get;set;}
};