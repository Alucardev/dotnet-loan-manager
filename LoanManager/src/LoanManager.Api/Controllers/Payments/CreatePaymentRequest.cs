namespace LoanManager.Api.Payments;

public record CreatePaymentRequest(
     Guid InstallmentId,
     int PaymentMethod
);