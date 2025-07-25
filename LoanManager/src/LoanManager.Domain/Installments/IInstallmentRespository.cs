using LoanManager.Domain.Abstractions;
namespace LoanManager.Domain.Installments;

public interface IInstallmentRespository
{
    Task<Installment?> GetByIdAsync(InstallmentId id, CancellationToken cancellationToken = default);
    void Add(Installment installment);
    Task<IReadOnlyList<Installment>> GetAllWithSpec(ISpecification<Installment,InstallmentId> spec);
    Task<int> CountAsync(ISpecification<Installment, InstallmentId> spec);
}