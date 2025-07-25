using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Shared;

namespace LoanManager.Domain.Permissions;


public sealed class Permission : Entity<PermissionId>
{

    public Name? Name { get; init; } 

    private Permission()
    {

    }

    public Permission(PermissionId id, Name name): base(id)
    {
        Name = name;
    }

    public Permission(Name name) : base()
    {
        Name = name;
    }
    

    public static Result<Permission> Create(Name name)
    {
        return new Permission(name);
    }

}