using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;


namespace LoanManager.Application.Metrics.GetGenerallyMetricsQuery;

internal sealed class GetGenerallyMetricsQueryHandler : IQueryHandler<GetGenerallyMetricsQuery, GetGenerallyMetricsQueryResponse>
{

     private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetGenerallyMetricsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
         _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<GetGenerallyMetricsQueryResponse>> Handle(GetGenerallyMetricsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = @"
                    SELECT 
                    total_clients AS ""TotalClients"",
                    expired_installments AS ""ExpiredInstallments"",
                    active_loans AS ""ActiveLoans"",
                    total_installments AS ""TotalInstallments"",
                    total_charged AS ""TotalCharged"",
                    total_loans_amount AS ""TotalLoansAmount""
                    FROM get_general_metrics(@CurrencyType, @PaymentMethod, @FromDate, @ToDate);
          ";
        var metrics = await connection.QueryFirstOrDefaultAsync<GetGenerallyMetricsQueryResponse>(sql, new {
            CurrencyType = request.CurrencyType,
            PaymentMethod = request.PaymentMethod,
            FromDate = request.FromDate,
            ToDate = request.ToDate
        });
        return metrics!;
    }
}
