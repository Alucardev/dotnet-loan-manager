using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Application.Metrics.GetGenerallyMetricsQuery;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;

namespace LoanManager.Api.Controllers.Metrics;

[ApiController]
[Route("api/metrics")]
public class MetricsController : ControllerBase
{
    private readonly ISender _sender; 

    public MetricsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> GetGenerallyMetrics(
        [FromQuery] GetGenerallyMetricsRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new GetGenerallyMetricsQuery(
            request.CurrencyType,
            request.PaymentMethod,
            request.FromDate,
            request.ToDate
        );
        var result = await _sender.Send(command, cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }
}
