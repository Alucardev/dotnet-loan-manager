using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Api.Controllers.Prestamos;
using LoanManager.Application.Loans.CreateLoans;
using LoanManager.Application.Loans.GetLoans;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;
using LoanManager.Application.Loans.GetLoansById;

namespace LoanManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ISender _sender; 

    public LoansController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> GetLoanById(
        CancellationToken cancellationToken,
        [FromRoute] Guid id
    )
    {
        var command = new GetLoanByIdQuery(id);
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }


    [HttpGet()]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]

    public async Task<ActionResult<PagedDapperResults<GetLoansResponse>>> GetPrestamosDapper(
        [FromQuery] GetLoansQuery request
    )
    {
        var resultados = await _sender.Send(request);   
        if(resultados.IsFailure) return BadRequest(resultados.Error);
        return Ok(resultados.Value);
    }



    [HttpPost]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> CreateLoan(
        CancellationToken cancellationToken,
        CreateLoanRequest request
    )
    {
        var command = new CreateLoanCommand(
            request.EmissionDate,
            request.ClientId,
            request.PlanId,
            request.Amount,
            request.CurrencyType,
            request.description
            );
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }

}