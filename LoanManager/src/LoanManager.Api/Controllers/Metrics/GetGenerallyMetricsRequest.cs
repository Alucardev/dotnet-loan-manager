namespace LoanManager.Api.Controllers.Metrics;

public sealed class GetGenerallyMetricsRequest
{
    public string CurrencyType { get; set; }
    public int PaymentMethod { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}