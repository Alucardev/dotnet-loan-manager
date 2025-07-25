using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Payments.CraeatePayment;

public record CreatePaymentCommand(
     Guid InstallmentId,
     int PaymentMethod
):ICommand<Guid>;   

