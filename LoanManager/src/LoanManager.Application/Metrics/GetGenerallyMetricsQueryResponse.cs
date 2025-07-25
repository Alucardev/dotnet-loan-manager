namespace LoanManager.Application.Metrics.GetGenerallyMetricsQuery;

public sealed class GetGenerallyMetricsQueryResponse
{
    public long TotalClients { get; set; }
    public long ExpiredInstallments { get; set; }
    public long ActiveLoans { get; set; }
    public long TotalInstallments { get; set; }
    public decimal TotalCharged { get; set; }
    public decimal TotalLoansAmount { get; set; }
}
