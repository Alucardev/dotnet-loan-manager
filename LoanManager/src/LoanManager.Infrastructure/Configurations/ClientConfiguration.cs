
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Clients;
using LoanManager.Domain.Shared;

namespace LoanManager.Infrastructure.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        builder.HasKey(Client => Client.Id);

        builder.Property(Client => Client.Id)
        .HasConversion(ClienteId => ClienteId.Value, Value => new ClientId(Value));

        builder.Property(Client => Client.Name)
        .HasMaxLength(200)
        .HasConversion(nombre => nombre!.Value, value => new Name(value));

        builder.Property(Client => Client.LastName)
        .HasMaxLength(200)
        .HasConversion(apellido => apellido!.Value, value => new LastName(value));

        builder.Property(Client => Client.Dni)
        .HasMaxLength(9)
        .HasConversion(dni => dni!.Value, value => new Dni(value));

        builder.Property(Client => Client.Phone)
        .HasMaxLength(17)
        .HasConversion(phone => phone!.Value, value => new Phone(value));
    }   
}