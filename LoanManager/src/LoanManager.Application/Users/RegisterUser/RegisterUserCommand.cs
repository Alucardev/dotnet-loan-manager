using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Users.RegisterUser;


public sealed record RegisterUserCommand(
    string Email,
     string Name, 
     string LastName, 
     string Password
): ICommand<Guid>;