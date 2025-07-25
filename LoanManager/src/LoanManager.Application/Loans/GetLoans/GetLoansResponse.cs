namespace LoanManager.Application.Loans.GetLoans;
    public class GetLoansResponse
    {
        public Guid Id { get; set; }
        public DateTime EmissionDate{ get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string AmountlCurrencyType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid PlanId { get; set; } 
    }

