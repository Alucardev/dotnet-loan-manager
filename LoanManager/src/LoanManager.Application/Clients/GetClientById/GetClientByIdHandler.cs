using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Clients.GetClienteById;
internal sealed class GetClientByIdQueryHandler : IQueryHandler<GetClientByIdQuery, GetClientByIdResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetClientByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
         _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<GetClientByIdResponse>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = """
             SELECT * FROM clients WHERE id = @Id
       """;
       var client = await connection.QueryFirstOrDefaultAsync<GetClientByIdResponse>(sql, new { request.Id});
       return client!;  
    }
}
