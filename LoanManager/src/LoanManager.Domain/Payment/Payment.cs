using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Shared;

namespace LoanManager.Domain.Payments;

public sealed class Payment : Entity<PaymentId>{

    private Payment(){ }

    private Payment(PaymentId id, InstallmentId installmentId, PaymentMethod paymentMethod ,Amount amount, DateTime paymentDate, Amount surcharge) 
    : base(id)
    {
        InstallmentId = installmentId;
        Amount = amount;
        PaymentMethod = paymentMethod;
        PaymentDate = paymentDate;
        Surcharge = surcharge;      
    }
    public InstallmentId InstallmentId {get; private set; } 
    public Amount Amount {get; private set; }
    public PaymentMethod PaymentMethod {get; private set; }
    public DateTime PaymentDate {get; private set; }
    public Amount Surcharge {get; private set; }

    public static Result<Payment> Create(Installment installment, PaymentMethod paymentMethod, DateTime paymentDate, Amount totalRecargo){
        
        var newPayment = new Payment(PaymentId.New(), installment.Id, paymentMethod, installment.Amount!, paymentDate, totalRecargo);
        var payInstallment = installment.Pay();
        if(payInstallment.IsFailure) return Result.Failure<Payment>(payInstallment.Error);
        return Result.Success(newPayment);
    }
}