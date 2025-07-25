
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Shared;
using LoanManager.Domain.Users;




namespace LoanManager.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(User => User.Id);

        builder.Property(User => User.Id)
        .HasConversion(UserId => UserId.Value, Value => new UserId(Value));

        builder.Property(User => User.Name)
        .HasMaxLength(200)
        .HasConversion(nombre => nombre.Value, value => new Name(value));

        builder.Property(User => User.LastName)
        .HasMaxLength(200)
        .HasConversion(apellido => apellido.Value, value => new LastName(value));

        builder.Property(user => user.Email)
        .HasMaxLength(400)
        .HasConversion(email => email!.Value, value => new Email(value));

        builder.Property(User => User.PasswordHash)
        .HasMaxLength(200)
        .HasConversion(password => password!.Value, value => new PasswordHash(value));

        builder.HasIndex(user => user.Email).IsUnique();
        
        builder.HasMany(x => x.Roles)
        .WithMany()
        .UsingEntity<UserRole>();
    }
}