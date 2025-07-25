using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Application.Installments.GetInstallments;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Installments.GetInstallmentsByLoanId;

internal sealed class GetInstallmentsByLoanIdHandler : IQueryHandler<GetInstallmentsByLoanIdQuery, List<GetInstallmentsResponse>> // hereda la clase QueryHandler creada ateriormente.
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetInstallmentsByLoanIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;   
    }

    // recibe una request que es de tipo QueryPlan y busca con el repositorio la Id que recibe y retorna el plan.
    public async Task<Result<List<GetInstallmentsResponse>>> Handle(GetInstallmentsByLoanIdQuery request, CancellationToken cancellationToken)
    {   
        using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = """
            SELECT 
                i.id as Id,
                i.loan_id as LoanId,
                i.installment_number as InstallmentNumber,
                i.expiration_date as ExpirationDate,
                i.status as Status,
                i.amount_total as TotalAmount
            FROM installments i 
            WHERE i.loan_id = @Id 
            ORDER BY i.expiration_date ASC
        """;

        var installments = await connection.QueryAsync<GetInstallmentsResponse>(sql, new { request.Id });
        return installments.ToList();
    }
}



