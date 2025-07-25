using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Api.Payments;
using LoanManager.Application.Payments.CalculatePayment;
using LoanManager.Application.Payments.CraeatePayment;
using LoanManager.Application.Payments.GetPaymentsByLoanId;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;



namespace LoanManager.Api.Controllers.Clientes;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly ISender _sender; 

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> CrearPago(
        CancellationToken cancellationToken,
        CreatePaymentRequest request
    )
    {
        var command = new CreatePaymentCommand(
            request.InstallmentId,
            request.PaymentMethod
         );
         
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }

        [HttpPost("Calculate")]
        [Authorize]
        [HasPermission(PermissionEnum.ReadUser)]
        [HasPermission(PermissionEnum.WriteUser)]   
        public async Task<IActionResult> CalculatePayment(
            CancellationToken cancellationToken,
            CalculatePaymentRequest request
        )
        {
            var command = new CalculatePaymentCommand(
                request.InstallmentId,
                request.PaymentMethod
            );
            
            var result = await _sender.Send(command,cancellationToken);   
            if(result.IsFailure) return BadRequest(result.Error);   
            return Ok(result.Value);
        }


        [HttpGet("GetPaymentsByLoanId/{id}")]
        [Authorize]
        [HasPermission(PermissionEnum.ReadUser)]
        [HasPermission(PermissionEnum.WriteUser)]   
        public async Task<IActionResult> GetPaymentByLoanId(
            CancellationToken cancellationToken,
            [FromRoute]GetPaymentsByLoanIdQuery request
        )
        {
            var query = new GetPaymentsByLoanIdQuery(
                request.Id
            );

            var result = await _sender.Send(query,cancellationToken);   
            if(result.IsFailure) return BadRequest(result.Error);   
            return Ok(result.Value);
        }


        
}