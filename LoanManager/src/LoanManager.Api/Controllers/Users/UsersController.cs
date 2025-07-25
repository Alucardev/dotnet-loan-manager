using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanManager.Application.Users.GetUserData;
using LoanManager.Application.Users.LoginUser;
using LoanManager.Application.Users.RegisterUser;
using LoanManager.Domain.Permissions;
using LoanManager.Infrastructure.Authentication;

namespace LoanManager.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender; 

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken
    )
    {
    
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        
        return Ok(result.Value);
    }

 
    [HttpPost("register")]
    //[HasPermission(PermissionEnum.WriteUser)]
    //[Authorize]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterUserCommand(
            request.email,
            request.name,
            request.lastname,
            request.Password
        );

        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [Authorize]
    [HasPermission(PermissionEnum.ReadUser)]
    [HasPermission(PermissionEnum.WriteUser)]
    public async Task<IActionResult> GetUserData(
        CancellationToken cancellationToken)
    {
        
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var request = new GetUserDataQuery(token);
        var result = await _sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);  
        }

        return Ok(result.Value);  
    }

}


