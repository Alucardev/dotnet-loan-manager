namespace LoanManager.Application.Installments.GetInstallments;

public sealed class GetInstallmentsQueryResponse
{
    public Guid id { get; set; }
    public Guid LoanId { get; set; }
    public int installmentNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string AmountCurrencyType { get; set; }
    public int Status { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalInstallments { get; set; }
    public string ClientName { get; set; }
    public string Dni { get; set; }
}
