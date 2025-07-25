using System.Text;
using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Clients.GetClients
{
    internal sealed class GetClientsQueryHandler : IQueryHandler<GetClientsQuery, PagedDapperResults<GetClientsResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetClientsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<PagedDapperResults<GetClientsResponse>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var builder = new StringBuilder("""
                SELECT 
                    cli.id,
                    cli.name as Name,
                    cli.last_name as LastName,
                    cli.phone as Phone,
                    cli.dni as Dni
                FROM Clients cli
            """);
            // Aquí falta el resto de la lógica para ejecutar la consulta
            var search = string.Empty;
            var whereStatement = string.Empty;

            if(!string.IsNullOrEmpty(request.Search))
            {
                search = "%" + EncodeForLike(request.Search) + "%";
                whereStatement = $" WHERE cli.name ILIKE @Search OR cli.dni ILIKE @Search OR cli.last_name ILIKE @Search"; 
                builder.AppendLine(whereStatement);
            }
            
            var orderBy = request.OrderBy;
            if(!string.IsNullOrEmpty(orderBy))
            {
                var orderStatement = string.Empty;
                var orderAsc = request.OrderAsc ? "ASC" : "DESC";
                switch(orderBy)
                {
                    case "nombre": orderStatement=$" ORDER BY cli.name {orderAsc}"; break;
                    default : orderStatement=$" ORDER BY cli.dni {orderAsc}"; break;
                }
                builder.AppendLine(orderStatement);
            }


             builder.AppendLine(" LIMIT @PageSize OFFSET @Offset;");


                builder.AppendLine("""
                    SELECT 
                        COUNT(*)
                 FROM Clients cli
            """);

            if(!string.IsNullOrEmpty(whereStatement)) builder.AppendLine(whereStatement);
            builder.AppendLine(";");

            var offset = request.PageSize * (request.PageNumber - 1);
            var sql = builder.ToString();

            Console.WriteLine(sql);
            using var multi = await connection.QueryMultipleAsync(sql, 
                new {
                    PageSize = request.PageSize,
                    Offset = offset,
                    Search = search
                }
            );

          var items = await multi.ReadAsync<GetClientsResponse>().ConfigureAwait(false);
          var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);
          var result = new PagedDapperResults<GetClientsResponse>(totalItems, request.PageNumber, request.PageSize)
          {
                Items = items
          };

          return result;
        }

    private string EncodeForLike(string search)
    {
        return search.Replace("[", "[]]").Replace("%", "[%]");
    }
        
    }
}
