using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Application.Plans.GetPlanById;

namespace  LoanManager.Application.Plans.GetPlanes;
public sealed record GetPlansQuery () :  IQuery<List<PlanResponse>>;
