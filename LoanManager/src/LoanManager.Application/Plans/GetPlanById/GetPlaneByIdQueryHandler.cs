
using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Plans.GetPlanById;

internal sealed class GetPlanByIdQueryHandler : IQueryHandler<GetPlanByIdQuery, PlanResponse> // hereda la clase QueryHandler creada ateriormente.
{
    
      private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetPlanByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;   
    }

    // recibe una request que es de tipo QueryPlan y busca con el repositorio la Id que recibe y retorna el plan.
    public async Task<Result<PlanResponse>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {
       using var connection = _sqlConnectionFactory.CreateConnection();
       var sql = """
             SELECT 
                id,
                total_installments as TotalInstallments,
                interest as Interest,
                penalty as Penalty,
                frequency as Frequency
              FROM plans WHERE id = @Id
       """;

       var plan = await connection.QueryFirstOrDefaultAsync<PlanResponse>(sql, new { request.Id });
       return plan!;
    }
}



