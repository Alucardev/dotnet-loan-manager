using Microsoft.AspNetCore.Authorization;

namespace LoanManager.Infrastructure.Authentication;


public class PermissionRequeriment : IAuthorizationRequirement
{
    public PermissionRequeriment(string? permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}