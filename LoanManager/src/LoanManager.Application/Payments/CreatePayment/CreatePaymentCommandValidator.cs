using FluentValidation;



namespace LoanManager.Application.Payments.CraeatePayment;


internal sealed class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(c => c.InstallmentId).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.PaymentMethod).NotEmpty().WithMessage("El campo no puede ser nulo");
    }
}