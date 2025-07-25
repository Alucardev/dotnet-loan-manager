using LoanManager.Domain.Clients;

namespace LoanManager.Infrastructure.Repositories;

internal sealed class ClientRepository: Repository<Client, ClientId>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}