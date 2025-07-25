using System.Text;
using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;


namespace LoanManager.Application.Loans.GetLoans;

internal sealed class GetLoansQueryHandler : IQueryHandler<GetLoansQuery, PagedDapperResults<GetLoansResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetLoansQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<PagedDapperResults<GetLoansResponse>>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var builder = new StringBuilder("""
            SELECT lo.id,
                   lo.emission_date as EmissionDate,
                   cli.last_name as LastName,
                   cli.dni as Dni,
                   cli.name as Name, 
                   lo.amount_total as Amount,
                   lo.amount_currency_type as AmountCurrencyType,
                   lo.status as Status,
                   lo.description as Description,
                   pla.id as planId
            FROM Loans lo
            LEFT JOIN clients cli ON lo.client_id = cli.id
            LEFT JOIN plans pla ON pla.id = lo.plan_id
        """);

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
                case "name": orderStatement=$" ORDER BY cli.name {orderAsc}"; break;
                case "amount": orderStatement=$" ORDER BY lo.amount_total {orderAsc}"; break;
                default : orderStatement=$" ORDER BY lo.emission_date {orderAsc}"; break;
            }
            builder.AppendLine(orderStatement);
        }   

         builder.AppendLine(" LIMIT @PageSize OFFSET @Offset;");


            builder.AppendLine("""
                SELECT 
                    COUNT(*)
             FROM Loans lo
            LEFT JOIN clients cli ON lo.client_id = cli.id
            LEFT JOIN plans pla ON pla.id = lo.plan_id
        """);

        if(!string.IsNullOrEmpty(request.Search))
        {
          builder.AppendLine(whereStatement);
          builder.AppendLine(";");  
        }
 

        var offset = request.PageSize * (request.PageNumber - 1);
        var sql = builder.ToString();
        using var multi = await connection.QueryMultipleAsync(sql, 
            new {
                PageSize = request.PageSize,
                Offset = offset,
                Search = search
            }
        );

      var items = await multi.ReadAsync<GetLoansResponse>().ConfigureAwait(false);
      var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);
      var result = new PagedDapperResults<GetLoansResponse>(totalItems, request.PageNumber, request.PageSize)
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

