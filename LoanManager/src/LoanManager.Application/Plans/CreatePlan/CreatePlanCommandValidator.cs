using FluentValidation;

namespace LoanManager.Application.Plans.CreatePlans;


internal sealed class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(c => c.TotalInstallments).NotEmpty().WithMessage("El campo no puede ser nulo.");
        RuleFor(c => c.Interest).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.Penalty).NotEmpty().WithMessage("El campo no puede ser nulo");
        RuleFor(c => c.frequency).NotEmpty().WithMessage("El campo no puede ser nulo");
    }
}