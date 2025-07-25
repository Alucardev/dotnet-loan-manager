using LoanManager.Domain.Plans;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Shared;

namespace LoanManager.Domain.Installments
{
    public class InstallmentCalculatorService
    {
        private List<Installment> InstallmentsCreated;

        public InstallmentCalculatorService()
        {
            InstallmentsCreated = new List<Installment>();
        }

        public IReadOnlyCollection<Installment> Calculate(Plan plan, Loan loan, DateTime date)
        {
            DateTime firstExpiration = new DateTime(
                date.Year, 
                date.Month, 
                date.Day,
                date.Hour,        
                date.Minute,      
                date.Second,     
                DateTimeKind.Utc
            ).AddMonths(1);

            decimal amount = (loan.Amount.Total / plan.TotalInstallments!.Value) * (1 + (plan.Interest!.Value / 100));

            for (int i = 0; i < plan.TotalInstallments!.Value; i++)
            {
                // Sumar la frecuencia al vencimiento y actualizar la fecha
                firstExpiration = firstExpiration.AddDays(plan.Frequency!.Value);
                
                var nueva = Installment.Create(
                    firstExpiration,
                    new Amount(amount, loan.Amount.CurrencyType),
                    new InstallmentNumber(i + 1),
                    loan
                );

                InstallmentsCreated.Add(nueva);
            }

            return InstallmentsCreated.AsReadOnly();
        }
    }
 }



