using LoanManager.Domain.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace LoanManager.Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionEnum permission):
     base(policy: permission.ToString()) 
    {

    }
}