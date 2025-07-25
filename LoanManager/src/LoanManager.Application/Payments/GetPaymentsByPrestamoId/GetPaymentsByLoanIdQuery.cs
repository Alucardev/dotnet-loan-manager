using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Payments.GetPaymentsByLoanId;
public sealed record GetPaymentsByLoanIdQuery(Guid Id) :IQuery<List<GetPaymentsByLoanIdResponse>>;