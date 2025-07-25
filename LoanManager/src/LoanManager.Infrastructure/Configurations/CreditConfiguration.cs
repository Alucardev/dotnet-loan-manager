using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Clients;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Plans;
using LoanManager.Domain.Shared;




namespace LoanManager.Infrastructure.Configurations;

internal sealed class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("loans");
        builder.HasKey(Loan => Loan.Id);

        builder.Property(Loan => Loan.Id)
        .HasConversion(LoanId => LoanId.Value, Value => new LoanId(Value));

          builder.Property(Loan => Loan.Description)
        .HasConversion(Descripcion => Descripcion.Value, Value => new Description(Value));

        builder.OwnsOne(Loan => Loan.Amount, amountBuilder =>
        {
            amountBuilder.Property(amount => amount.CurrencyType)
            .HasConversion(CurrencyType => CurrencyType.Code, codigo => CurrencyType.FromCodigo(codigo!));
        });
        
        //Foregin Key's
        builder.HasOne<Client> ().WithMany().HasForeignKey(loan => loan.ClientId);
        builder.HasOne<Plan> ().WithMany().HasForeignKey(loan => loan.PlanId);

    }   
}