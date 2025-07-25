
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Plans;



namespace LoanManager.Infrastructure.Configurations;

internal sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("plans");
        builder.HasKey(Plan => Plan.Id);

        builder.Property(Plan => Plan.Id)
        .HasConversion(PlanId => PlanId.Value, Value => new PlanId(Value));

         builder.Property(Plan => Plan.Frequency)
        .HasConversion(Frecuency => Frecuency.Value, Value => new Frequency(Value));

        builder.Property(Plan => Plan.TotalInstallments)
        .HasConversion(TotalInstallments => TotalInstallments!.Value, value => new TotalInstallments(value));

        builder.Property(Plan => Plan.Interest)
        .HasConversion(interest => interest!.Value, value => new Interest(value));

        builder.Property(Plan => Plan.Penalty)
        .HasConversion(penalty => penalty!.Value, value => new Penalty(value));
        
    }
}