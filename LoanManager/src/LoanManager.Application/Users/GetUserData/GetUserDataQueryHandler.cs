
using Dapper;
using LoanManager.Application.Abstractions.Authentication;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Users.GetUserData;
internal sealed class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, GetUserDataQueryResponse>
{

    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IJwtProvider _jwtProvider;

    public GetUserDataQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IJwtProvider jwtProvider)
    {
    _sqlConnectionFactory = sqlConnectionFactory;
    _jwtProvider = jwtProvider;
    }

    public async Task<Result<GetUserDataQueryResponse>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {

        var UserId = new Guid(_jwtProvider.Decode(request.token));
        
        using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = """
          SELECT u.name, u.lastname from users u where id = @UserId
        """;

        Console.WriteLine(sql);

        var data = await connection.QueryFirstOrDefaultAsync<GetUserDataQueryResponse>(sql, new { UserId });
        return data!;
    }
}
