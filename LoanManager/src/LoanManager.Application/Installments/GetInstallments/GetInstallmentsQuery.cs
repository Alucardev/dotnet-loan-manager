using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Shared;

namespace LoanManager.Application.Installments.GetInstallments;


public sealed record GetInstallmentsQuery 
    : PaginationParams, 
      IQuery<PagedDapperResults<GetInstallmentsQueryResponse>>
{
    public DateTime? EndDate { get; init; }
    public DateTime? StartDate { get; init; }
    public int? Status { get; init; }  
}
