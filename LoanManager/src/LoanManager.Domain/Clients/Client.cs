using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Clients.Events;
using LoanManager.Domain.Shared;

namespace LoanManager.Domain.Clients;



// Creamos la clase que hereda entity y le pasamos el tipo de Identificador que tiene el cliente.
// Con esto recibimos algunas propiedades por ser un Entidad - ver la clase entity
public sealed class Client : Entity<ClientId>
{
    // constructor vacio para no tener warnings / problemas con los EntityFramework
    private Client() {

    }

    //Constructor
     private Client (
        ClientId id,
        LastName lastname,
        Name name,
        Dni dni,
        Phone phone
     ) : base(id)
     {
         LastName = lastname;
         Name = name;
         Dni = dni;
         Phone = Phone;
     }
     public LastName? LastName {get; private set;}
     public Name? Name {get; private set;}
     public Dni? Dni{get; private set;}
     public Phone? Phone {get; private set;}

    // creamos el cliente y mandamos el evento de dominio.
     public static Client Create(
        LastName lastname,
        Name name,
        Dni dni,
        Phone phone
     )
     {
        var newClient = new Client(ClientId.New(), lastname, name, dni, phone);

        // enviamos evento de dominio al crear el cliente.
        newClient.RaiseDomainEvent(new ClientCreatedDomainEvent(newClient.Id));
        return newClient;
     }
}


