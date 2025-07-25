using LoanManager.Domain.Abstractions;

namespace LoanManager.Domain.Users;
public static class UserErrors {
    public static Error NotFound = new ("User.Found","No existo el usuario con esta id");
    public static Error InvalidCredentials = new("User.InvalidCredentials", "Las credenciales son incorrectas");
    public static Error AlreadyExists = new (
        "User.AlreadyExists",
        "El usuario ya existe en la base de datos"
    );
}