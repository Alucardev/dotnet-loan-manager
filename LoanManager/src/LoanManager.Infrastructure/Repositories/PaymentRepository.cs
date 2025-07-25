
using LoanManager.Domain.Payments;

namespace LoanManager.Infrastructure.Repositories;

internal sealed class PaymentRepository: Repository<Payment, PaymentId>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}

