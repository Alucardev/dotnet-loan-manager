namespace LoanManager.Application.Payments.GetPaymentsByLoanId;


public sealed class GetPaymentsByLoanIdResponse
{
    public Guid id { get; set; }
    public Guid InstallmentId { get; set; }
    public int PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
    public Decimal TotalAmount { get; set; }
    public Decimal SurchargeTotal { get; set; }
    public string AmountCurrencyType { get; set; }
}

