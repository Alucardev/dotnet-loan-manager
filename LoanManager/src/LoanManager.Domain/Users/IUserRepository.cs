namespace LoanManager.Domain.Users;


public interface IUserRepository
{
    Task<User?> GetByIdAsync(User id, CancellationToken cancellationToken = default);
    void Add(User user);

    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    
    Task<bool> IsUserExists(
        Email email,
        CancellationToken cancellationToken = default
    );
}