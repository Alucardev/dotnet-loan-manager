using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Loans;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(LoanId id, CancellationToken cancellationToken = default);
    void Add(Loan prestamo);
    Task<IReadOnlyList<Loan>> GetAllWithSpec(ISpecification<Loan,LoanId> spec);
    Task<int> CountAsync(ISpecification<Loan, LoanId> spec);
}