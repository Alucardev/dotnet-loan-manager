using System.Text;
using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Installments.GetInstallments
{
    internal sealed class GetInstallmentsQueryHandler : IQueryHandler<GetInstallmentsQuery, PagedDapperResults<GetInstallmentsQueryResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetInstallmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<PagedDapperResults<GetInstallmentsQueryResponse>>> Handle(GetInstallmentsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var builder = new StringBuilder("""
                SELECT 
                    i.id,
                    i.installment_number as InstallmentNumber,
                    i.expiration_date as ExpirationDate,
                    l.id as LoanId,
                    i.status as Status,
                    i.amount_total as AmountTotal,
                    i.amount_currency_type as AmountCurrencyType,
                    pla.total_installments as TotalInstallments,
                    CONCAT(cli.name, ' ', cli.last_name) AS ClientName,
                    cli.dni as Dni
                FROM 
                    installments i
                JOIN loans l ON i.loan_id = l.id
                JOIN clients cli ON l.client_id = cli.id
                JOIN plans pla ON l.plan_id = pla.id
            """);
            // Aquí falta el resto de la lógica para ejecutar la consulta

 
             var conditions = new List<string>();

            if (request.StartDate != null)
            {
                conditions.Add("i.expiration_date > @From");
            }

            if (request.EndDate != null)
            {
                conditions.Add("i.expiration_date < @To");
            }

            if (request.Status != null)
            {
                conditions.Add("i.status = @Status");
            }


               // Si hay condiciones, las agregamos al WHERE
            if (conditions.Any())
            {
                builder.AppendLine(" WHERE " + string.Join(" AND ", conditions));
            }

            builder.AppendLine(" LIMIT @PageSize OFFSET @Offset;");

            builder.AppendLine("""
                    SELECT 
                        COUNT(*)
                 FROM 
                    installments i
                JOIN loans l ON i.loan_id = l.id
                JOIN clients cli ON l.client_id = cli.id
                JOIN plans pla ON l.plan_id = pla.id
            """);

            var orderBy = request.OrderBy;
            if(!string.IsNullOrEmpty(orderBy))
            {
                var orderStatement = string.Empty;
                var orderAsc = request.OrderAsc ? "ASC" : "DESC";
                switch(orderBy)
                {
                    case "name": orderStatement=$" ORDER BY cli.name {orderAsc}"; break;
                    case "amount": orderStatement=$" ORDER BY i.amount_total {orderAsc}"; break;
                    default : orderStatement=$" ORDER BY cli.dni {orderAsc}"; break;
                }
                builder.AppendLine(orderStatement);
            }
            
             if (conditions.Any())
            {
                builder.AppendLine(" WHERE " + string.Join(" AND ", conditions));
            }

            builder.AppendLine(";");  
            
    
            var offset = request.PageSize * (request.PageNumber - 1);
            var sql = builder.ToString();
            Console.WriteLine(sql);
            using var multi = await connection.QueryMultipleAsync(sql, 
                new {
                    PageSize = request.PageSize,
                    Offset = offset,
                    From = request.StartDate,
                    To = request.EndDate,
                    Status = request.Status
                }
            );

            

          var items = await multi.ReadAsync<GetInstallmentsQueryResponse>().ConfigureAwait(false);
          var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);
          var result = new PagedDapperResults<GetInstallmentsQueryResponse>(totalItems, request.PageNumber, request.PageSize)
          {
                Items = items
          };

          return result;
        }        
    }
}
