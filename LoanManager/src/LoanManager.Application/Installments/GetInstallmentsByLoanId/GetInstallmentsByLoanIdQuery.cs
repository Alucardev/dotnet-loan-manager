using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Installments.GetInstallmentsByLoanId;


public sealed record GetInstallmentsByLoanIdQuery (Guid Id ) :  IQuery<List<GetInstallmentsResponse>>;
