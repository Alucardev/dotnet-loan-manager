using FluentValidation;


namespace LoanManager.Application.Users.RegisterUser;


internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("El nombre no puede ser nulo.");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("Los Apellidos no pueden ser nulos");
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);
    }
}