using LoanManager.Domain.Installments;

namespace LoanManager.Infrastructure.Repositories;

internal sealed class InstallmentRepository: Repository<Installment, InstallmentId>, IInstallmentRespository
{
    public InstallmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}

