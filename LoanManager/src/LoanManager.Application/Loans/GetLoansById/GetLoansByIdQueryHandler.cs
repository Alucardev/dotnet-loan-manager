using Dapper;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Abstractions;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Abstractions.Messaging;

namespace LoanManager.Application.Loans.GetLoansById;
internal sealed class GetLoanByIdQueryHandler : IQueryHandler<GetLoanByIdQuery, GetLoanByIdResponse>
{
    private readonly ILoanRepository _loanRepository;
  

    public GetLoanByIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<Result<GetLoanByIdResponse>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(new LoanId(request.Id), cancellationToken);

        if (loan is null)
        {
            return Result.Failure<GetLoanByIdResponse>(LoanErrors.NotFound);
        }

        var response = new GetLoanByIdResponse
        {
            Id = loan.Id.Value,
            Plan_Id = loan.PlanId.Value,
            Cliente_Id = loan.ClientId.Value,
            Descripcion = loan.Description.Value,
            Fecha_Emision = loan.EmissionDate,
            Capital_Total = loan.Amount.Total,
            Capital_Tipo_Moneda = loan.Amount.CurrencyType.Code,
            Estado = loan.Status.ToString()
        };

        return response;
    }
}   