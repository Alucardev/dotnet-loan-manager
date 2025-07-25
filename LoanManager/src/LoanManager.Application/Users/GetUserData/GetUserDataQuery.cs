
using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Users.GetUserData;

public sealed record GetUserDataQuery(string token) : IQuery<GetUserDataQueryResponse>;
