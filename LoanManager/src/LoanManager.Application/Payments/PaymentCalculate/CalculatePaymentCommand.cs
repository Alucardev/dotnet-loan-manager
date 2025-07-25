using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Payments.CalculatePayment;

public record CalculatePaymentCommand(
     Guid InstallmentId,
     int PaymentMethod
):ICommand<CalculatePaymentResponse>;   

