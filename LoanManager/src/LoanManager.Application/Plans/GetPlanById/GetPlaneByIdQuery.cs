using LoanManager.Application.Abstractions.Messaging;

namespace  LoanManager.Application.Plans.GetPlanById;
public sealed record GetPlanByIdQuery (Guid Id ) :  IQuery<PlanResponse>;
