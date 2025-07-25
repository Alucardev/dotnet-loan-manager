using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Application.Plans.CreatePlans;
using LoanManager.Application.Plans.DeletePlan;
using LoanManager.Application.Plans.GetPlanById;
using LoanManager.Application.Plans.GetPlanes;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;

namespace LoanManager.Api.Controllers.Plans;


[ApiController]
[Route("api/plans")]
public class PlansController : ControllerBase
{
    private readonly ISender _sender; 

    public PlansController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> GetPlanById(
        CancellationToken cancellationToken,
        [FromRoute] Guid id
    )
    {
        var command = new GetPlanByIdQuery(id);
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> CreatePlan(
        CancellationToken cancellationToken,
        CreatePlanRequest request
    )
    {
        var command = new CreatePlanCommand(
         request.totalInstallments,
         request.interest,
         request.penalty, 
         request.frequency
         );
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }

        [HttpDelete("{id}")]
        [Authorize]
        [HasPermission(PermissionEnum.ReadUser)]
        [HasPermission(PermissionEnum.WriteUser)]   
        public async Task<IActionResult> DeletePlan(
            CancellationToken cancellationToken,
            Guid id
        )
        {
            var command = new DeletePlanCommand(id);
            var result = await _sender.Send(command,cancellationToken);   
            if(result.IsFailure) return BadRequest(result.Error);   
            return Ok(result.Value);
        }


    [HttpGet]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> GetPlans(
        CancellationToken cancellationToken
    )
    {
        var command = new GetPlansQuery();
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }

}