using MediatR;
using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Application.Installments.GetInstallmentsByLoanId;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Loans;


namespace LoanManager.Application.Loans.CheckLoanCompleted;


public class CheckloanCompletedQueryHandler : IQueryHandler<CheckLoanCompletedQuery, bool>
{
    private readonly ILoanRepository _prestamoRepository;
    private readonly ISender _sender;

    public CheckloanCompletedQueryHandler(ILoanRepository prestamoRepository, ISender sender)
    {
        _prestamoRepository = prestamoRepository;
        _sender = sender;
    }

    public async Task<Result<bool>> Handle(CheckLoanCompletedQuery request, CancellationToken cancellationToken)
    {
        // Obtener el préstamo de forma asincrónica
        var loan = await _prestamoRepository.GetByIdAsync(new LoanId(request.LoanId));
        
        if (loan == null)
        {
            return Result.Failure<bool>(LoanErrors.NotFound);
        }
        // Obtener las cuotas asociadas al préstamo de forma asincrónica
        var installmentsResult = await _sender.Send(new GetInstallmentsByLoanIdQuery(loan.Id.Value), cancellationToken);

        // Verificar si la consulta de cuotas fue exitosa
        if (installmentsResult.IsFailure)
        {
            return Result.Failure<bool>(InstallmentErrors.NotFound);
        }
        var installments = installmentsResult.Value;
        // Comprobar si alguna cuota está pendiente

         bool allPaid = installments.All(c => c.Status == ((int)InstallmentStatus.Payed).ToString());
     
        // Si todas las cuotas están pagadas, actualizar el estado del préstamo
        if (allPaid)
        {
            return Result.Success<bool>(true);
        }

        return Result.Success<bool>(false);
    }
}
