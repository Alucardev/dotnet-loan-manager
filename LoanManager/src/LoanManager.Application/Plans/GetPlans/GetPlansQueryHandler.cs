using Dapper;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Application.Plans.GetPlanById;
using LoanManager.Domain.Abstractions;

namespace LoanManager.Application.Plans.GetPlanes;

internal sealed class GetPlansQueryHandler : IQueryHandler<GetPlansQuery, List<PlanResponse>> // hereda la clase QueryHandler creada ateriormente.
{
    
      private readonly ISqlConnectionFactory _sqlConnectionFactory;


    public GetPlansQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;   
    }

    // recibe una request que es de tipo QueryPlan y busca con el repositorio la Id que recibe y retorna el plan.
    public async Task<Result<List<PlanResponse>>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
       using var connection = _sqlConnectionFactory.CreateConnection();
       var sql = """
             SELECT 
             id,
             total_installments as TotalInstallments,
             interest as Interest,
             penalty as Penalty,
             frequency as Frequency
              FROM plans 
       """;

       var planes = await connection.QueryAsync<PlanResponse>(sql);
       return planes.ToList();
    }
}



