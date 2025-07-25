using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Roles;
using LoanManager.Domain.Shared;

namespace LoanManager.Domain.Users;

public sealed class User : Entity<UserId>
{
    private readonly List<Role> _roles = new();
    private User(){}

    private User(UserId id,Name name, LastName lastName,PasswordHash passwordHash, Email email): base(id){
        Name = name;
        LastName = lastName;
        PasswordHash = passwordHash;
        Email = email;
    }

    public LastName LastName { get; set; }
    public Name Name { get; private set; }
    public PasswordHash PasswordHash {get;private set;}
    public Email Email{get;private set;}

    public static User Create(Name Name, LastName lastName,PasswordHash passwordHash, Email email){
        var newUser = new User(UserId.New(),Name, lastName, passwordHash, email);

        newUser.RaiseDomainEvent(new UserCreatedDomainEvent(newUser.Id));
        newUser._roles.Add(Role.Client);
        newUser._roles.Add(Role.Admin);
        return newUser;
    }

    public IReadOnlyCollection<Role>? Roles => _roles.ToList(); 
}