using FluentValidation;

namespace LoanManager.Application.Clients.CreateClient;


internal sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(c => c.LastName).NotEmpty().WithMessage("El campo no puede ser nulo").MinimumLength(2).MaximumLength(200);
        RuleFor(c => c.Name).NotEmpty().WithMessage("El campo no puede ser nulo").MinimumLength(3);
        RuleFor(c => c.Dni).NotEmpty().WithMessage("El campo no puede ser nulo")
            .Matches("^[0-9]+$").WithMessage("El DNI debe ser un número válido");
        RuleFor(c => c.Phone).NotEmpty().WithMessage("El campo no puede ser nulo")
         .Matches("^[0-9]+$").WithMessage("El DNI debe ser un número válido");
    }
}

