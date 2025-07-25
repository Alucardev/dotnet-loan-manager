using FluentValidation;
namespace LoanManager.Application.Payments.CalculatePayment;

internal sealed class CalculatePaymentCommandValidator : AbstractValidator<CalculatePaymentCommand>
{
    public CalculatePaymentCommandValidator()
    {
        RuleFor(c => c.InstallmentId).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.PaymentMethod).NotEmpty().WithMessage("El campo no puede ser nulo");
    }
}