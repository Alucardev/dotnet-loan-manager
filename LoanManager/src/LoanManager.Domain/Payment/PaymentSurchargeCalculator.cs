using LoanManager.Domain.Installments;
using LoanManager.Domain.Plans;


namespace LoanManager.Domain.Payments;

public class PaymentSurchargeCalculator
{
    public decimal Calculate(Plan plan, Installment installment, DateTime paymentDate)
    {
        if(installment.ExpirationDate < paymentDate)
        {
            var pastDueDays = (installment.ExpirationDate - paymentDate).Days; //calcula los dias
            var Surcharge = (pastDueDays * (plan.Penalty!.Value / 100 )) * installment.Amount!.Total; //calcula el interes multiplicado por los dias pasados
            return Surcharge;
        }
        return 0;
    }
}






