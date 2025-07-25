using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Application.Installments.GetInstallments;
using LoanManager.Application.Installments.GetInstallmentsByLoanId;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;

namespace LoanManager.Api.Controllers.Clientes;

[ApiController]
[Route("api/installments")]
public class InstallmentsController : ControllerBase
{
    private readonly ISender _sender; 

    public InstallmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("GetInstallmentsByLoanId/{id}")]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<ActionResult<List<GetInstallmentsResponse>>> GetInstallmentByLoanId(
        [FromRoute] GetInstallmentsByLoanIdQuery request 
    )
    {
        var results = await _sender.Send(request);
        return Ok(results.Value);
    }

    [HttpGet]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]    
    public async Task<ActionResult<List<GetInstallmentsResponse>>> GetInstallments(
        [FromQuery] GetInstallmentsQuery request 
    )
    {
        var results = await _sender.Send(request);
        return Ok(results.Value);
    }
}

