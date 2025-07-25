using LoanManager.Domain.Abstractions;
using MediatR;

namespace LoanManager.Application.Abstractions.Messaging;


public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}