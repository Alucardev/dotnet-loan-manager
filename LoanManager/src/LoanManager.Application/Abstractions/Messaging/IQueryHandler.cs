using LoanManager.Domain.Abstractions;
using MediatR;

namespace LoanManager.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> :
 IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}