using LoanManager.Application.Exceptions;
using LoanManager.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Definición de la clase ApplicationDbContext, que hereda de DbContext y 
// implementa la interfaz IUnitOfWork
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    // Declaración de un public publisher que se utilizará para publicar eventos de dominio
    private readonly IPublisher _publisher;

    // Constructor que recibe opciones de DbContext y un public publisher
    // Inicializa el DbContext base y asigna el publisher
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    // Método sobreescrito para configurar el modelo de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas las configuraciones de entidad del ensamblado actual
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // Llama al método base para asegurar que se realicen configuraciones adicionales
        base.OnModelCreating(modelBuilder);
    }

    // Método sobreescrito para guardar cambios en la base de datos de forma asíncrona
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // Llama al método base para guardar los cambios y obtiene el resultado
            var result = await base.SaveChangesAsync(cancellationToken);

            // Publica los eventos de dominio después de guardar los cambios
            await PublishDomainEventsAsync();
            
            // Devuelve el número de entidades afectadas
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Captura excepciones de concurrencia y lanza una excepción personalizada
            throw new ConcurrencyException("Fallo en la concurrencia", ex);
        }
    }

    // Método privado para publicar eventos de dominio de las entidades modificadas
    private async Task PublishDomainEventsAsync()
    {
        // Obtiene las entidades que implementan IEntity y sus eventos de dominio
        var domainEvents = ChangeTracker
            .Entries<IEntity>() // Obtiene las entradas del ChangeTracker que son entidades
            .Select(entry => entry.Entity) // Selecciona las entidades
            .SelectMany(entity => {
                var domainEvents = entity.GetDomainEvents(); // Obtiene los eventos de dominio de cada entidad
                return domainEvents; // Devuelve los eventos de dominio como una colección
            }).ToList(); // Convierte a lista

        // Publica cada evento de dominio utilizando el publisher
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}

