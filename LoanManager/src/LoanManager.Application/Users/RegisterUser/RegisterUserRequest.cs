namespace LoanManager.Application.Users.RegisterUser;


public record RegisterUserRequest
(
    string email,
    string name,
    string lastname,
    string Password
);