using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Shared;
using LoanManager.Domain.Installments.Event;

namespace LoanManager.Domain.Installments;

public sealed class Installment : Entity<InstallmentId>
{
    private Installment(){

    }

    private Installment(InstallmentId id, DateTime expireDate, InstallmentStatus status, InstallmentNumber installmentNumber ,Amount amount, LoanId loanId) : base(id){
        ExpirationDate = expireDate;
        Status = status;
        Amount = amount;
        InstallmentNumber = installmentNumber;
        LoanId = loanId;
    }

    public DateTime ExpirationDate { get; private set;}
    public InstallmentStatus Status {get; internal set;}
    public InstallmentNumber InstallmentNumber {get; private set;}
    public Amount Amount {get; private set;}
    public LoanId LoanId {get; private set;}

    public static Installment Create(DateTime expirationDate, Amount amount, InstallmentNumber installmentNumber, Loan loan)
    {
        var newInstallment = new Installment(new InstallmentId(Guid.NewGuid()) ,expirationDate , InstallmentStatus.Pending, installmentNumber, amount, loan.Id);
        newInstallment.RaiseDomainEvent(new InstallmentCreatedDomainEvent(newInstallment.Id));
        return newInstallment;
    }

    public Result Pay()
    {
        if(Status != InstallmentStatus.Payed){
             Status = InstallmentStatus.Payed;
             return Result.Success();
        }
        return Result.Failure(InstallmentErrors.AlreadyPayed);
    }
}   