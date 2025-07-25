using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Application.Clients.CreateClient;
using LoanManager.Application.Clients.GetClienteById;
using LoanManager.Application.Clients.GetClients;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;


namespace LoanManager.Api.Controllers.Clientes;



[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly ISender _sender; 


    public ClientsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> GetClientById(
        CancellationToken cancellationToken,
        [FromRoute] Guid id
    )
    {
        var command = new GetClientByIdQuery(id);
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }


    [HttpPost]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> CreateClient(
        CancellationToken cancellationToken,
        CreateClientRequest request
    )
    {
        var command = new CreateClientCommand(request.LastName, request.Name, request.Dni, request.Phone);
        var result = await _sender.Send(command,cancellationToken);   
        if(result.IsFailure) return BadRequest(result.Error);   
        return Ok(result.Value);
    }

    [HttpGet]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [ProducesResponseType(typeof(PagedDapperResults<GetClientsResponse>),
        (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedDapperResults<GetClientsResponse>>> GetClients(
        [FromQuery] GetClientsQuery request 
    )
    {
        // Send the query to the mediator to execute the handler.
        var resultados = await _sender.Send(request);
        
        // If the result is a failure, return a 400 Bad Request with the error message.
        if(resultados.IsFailure) return BadRequest(resultados.Error);
        
        // If the result is a success, return a 200 OK with the paged result set.
        return Ok(resultados.Value);
    }

}