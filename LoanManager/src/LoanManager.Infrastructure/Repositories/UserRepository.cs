using Microsoft.EntityFrameworkCore;
using LoanManager.Infrastructure.Repositories;
using LoanManager.Domain.Users;

namespace CleanArchitecture.Infrastructure.Repositories;


internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public Task<User?> GetByIdAsync(User id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsUserExists(Email email, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
        .AnyAsync(x => x.Email == email);
    }



    // realizamos una sobrecarga del método Add para que al agregar un usuario, se agreguen también los roles
    public override void Add(User user)
    {
        foreach (var role in user.Roles)
        {
            DbContext.Attach(role);
        }
        DbContext.Add(user);
    }

}