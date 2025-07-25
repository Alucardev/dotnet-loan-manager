
using LoanManager.Application.Abstractions.Messaging;


namespace LoanManager.Application.Metrics.GetGenerallyMetricsQuery;

public sealed record GetGenerallyMetricsQuery (
    string CurrencyType,
    int PaymentMethod,
    DateTime FromDate,
    DateTime ToDate
) : IQuery<GetGenerallyMetricsQueryResponse>;

    

