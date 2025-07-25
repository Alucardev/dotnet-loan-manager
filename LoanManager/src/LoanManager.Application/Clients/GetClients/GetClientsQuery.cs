using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Shared;

namespace LoanManager.Application.Clients.GetClients;


public sealed record GetClientsQuery : PaginationParams, IQuery<PagedDapperResults<GetClientsResponse>>
{
    public string Search {get;set;}
};

