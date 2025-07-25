using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Users.LoginUser;

public record LoginCommand(string Email, string Password) :  ICommand<string>;

