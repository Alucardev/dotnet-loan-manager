using LoanManager.Domain.Permissions;

namespace LoanManager.Domain.Roles;


public sealed class RolePermission
{
    public int RoleId {get;set;}
    public PermissionId? PermissionId {get;set;}
}



