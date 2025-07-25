using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Loans.Events;
using LoanManager.Domain.Shared;
using LoanManager.Domain.Plans;
using LoanManager.Domain.Clients;

namespace LoanManager.Domain.Loans;
public sealed class Loan : Entity<LoanId>
{
	private Loan()
	{
	}
	
	
	private Loan(
		LoanId id,
		DateTime emissionDate,
		ClientId clienteid,
		Amount capital,
		PlanId planId,
		LoanStatus status,
		Description description
	) : base(id)
	{
		EmissionDate = emissionDate;
		ClientId = clienteid;
		Amount = capital;
		PlanId = planId;
		Status = status;
		Description= description;	
	}

    public DateTime EmissionDate { get; private set; }
    public ClientId ClientId { get; private set; }
    public Amount Amount { get; private set; }
    public PlanId PlanId { get; private set; }
    public LoanStatus Status { get; private set; }

	public Description Description{ get; private set; }



	public void Complete() => Status = LoanStatus.Completed;

	public static Loan Create(
		DateTime emissionDate,
		Client client,
		Amount amount,
		Plan plan,
		Description description
	)
	{
		var newLoan = new Loan(LoanId.New(), emissionDate, client.Id, amount, plan.Id, LoanStatus.Active, description);
		newLoan.RaiseDomainEvent(new LoanCreatedDomainEvent(newLoan.Id));
		return newLoan;
	}
}

