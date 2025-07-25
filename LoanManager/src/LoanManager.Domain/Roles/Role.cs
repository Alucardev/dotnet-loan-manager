using LoanManager.Domain.Shared;
using LoanManager.Domain.Permissions;

namespace LoanManager.Domain.Roles;

public sealed class Role : Enumeration<Role> {

    public Role(int id, string name) : base(id, name)
    {

    }

 

    public ICollection<Permission>? Permissions {get; set;}
    public static readonly Role Client = new(1, "Cliente");
    public static readonly Role Admin = new(2, "Admin");


}