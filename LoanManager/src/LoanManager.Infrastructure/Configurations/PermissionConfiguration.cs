using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Permissions;
using LoanManager.Domain.Shared;

namespace LoanManager.Infrastructure.Configurations;


public sealed class PermissionConfiguration: IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(p => p.Id );

        builder.Property(x => x.Id)
        .HasConversion(permissionId => permissionId.Value, value => new PermissionId(value));

        builder.Property(x => x.Name)
        .HasConversion(permissionName => permissionName.Value, value => new Name(value));

        IEnumerable<Permission> permissions = Enum.GetValues<PermissionEnum>()
        .Select(p => new Permission(
            new PermissionId((int)p), 
            new Name(p.ToString())
        ));

        builder.HasData(permissions);
    }   
}