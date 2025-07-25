using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Clients;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(ClientId id, CancellationToken cancellationToken = default);
    void Add(Client client);
    Task<IReadOnlyList<Client>> GetAllWithSpec(ISpecification<Client,ClientId> spec);
    Task<int> CountAsync(ISpecification<Client, ClientId> spec);
}