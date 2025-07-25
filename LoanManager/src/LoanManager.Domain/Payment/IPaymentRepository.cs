namespace LoanManager.Domain.Payments;
public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default);
    void Add(Payment payment);

}
