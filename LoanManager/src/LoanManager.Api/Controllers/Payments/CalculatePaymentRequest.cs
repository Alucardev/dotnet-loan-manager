namespace LoanManager.Api.Payments;

public record CalculatePaymentRequest(
     Guid InstallmentId,
     int PaymentMethod
);