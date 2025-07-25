using FluentValidation;
using LoanManager.Domain.Shared;

namespace LoanManager.Application.Loans.CreateLoans;


internal sealed class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(c => c.ClientId).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.PlanId).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.EmissionDate).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.CurrencyType).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.Amount).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.CurrencyType)
        .Must(tipo => CurrencyType.All.Any(t => t.Code == tipo))
        .WithMessage("El tipo de moneda es inválido.");

    }
}