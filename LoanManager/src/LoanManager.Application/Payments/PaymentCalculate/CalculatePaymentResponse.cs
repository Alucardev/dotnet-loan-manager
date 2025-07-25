

namespace LoanManager.Application.Payments.CalculatePayment;

public sealed record CalculatePaymentResponse
{
    public decimal Surcharge { get; set; }
    public decimal Total { get; set; }
}