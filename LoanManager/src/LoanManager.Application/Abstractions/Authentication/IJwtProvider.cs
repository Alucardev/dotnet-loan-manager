using LoanManager.Domain.Users;

namespace LoanManager.Application.Abstractions.Authentication;



public interface IJwtProvider
{
    Task<string> Generate(User user);
    string Decode(string userId);
}