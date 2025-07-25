using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Payments.GetPaymentsByLoanId;
internal sealed class GetPaymentsByLoanIdQueryHandler : IQueryHandler<GetPaymentsByLoanIdQuery, List<GetPaymentsByLoanIdResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetPaymentsByLoanIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<List<GetPaymentsByLoanIdResponse>>> Handle(GetPaymentsByLoanIdQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request);
      
        using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = """
          SELECT 
          p.id, 
          i.id as InstallmentId, 
          i.amount_total as TotalAmount,
          p.payment_date as PaymentDate, 
          i.amount_currency_type as AmountCurrencyType,
          p.payment_method as PaymentMethod,
          p.surcharge_total as SurchargeTotal
          FROM public.payments p, public.installments i 
          where i.loan_id = @Id 
          AND p.installment_id = i.id
        """;
        var pagos = await connection.QueryAsync<GetPaymentsByLoanIdResponse>(sql, new { request.Id });
    
        return pagos.ToList();
    }
}   
