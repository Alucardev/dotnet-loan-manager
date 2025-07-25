using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Loans;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Shared;



namespace LoanManager.Infrastructure.Configurations;

internal sealed class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.ToTable("installments");
        builder.HasKey(Installment => Installment.Id);

        builder.Property(Installment => Installment.Id)
        .HasConversion(InstallmentId => InstallmentId.Value, Value => new InstallmentId(Value));

        builder.Property(Installment => Installment.InstallmentNumber)
        .HasConversion(InstallmentNumber => InstallmentNumber.Value, Value => new InstallmentNumber(Value));

        builder.OwnsOne(Installment => Installment.Amount, capitalBuilder =>
        {
            capitalBuilder.Property(monto => monto.CurrencyType)
            .HasConversion(TipoMoneda => TipoMoneda.Code, codigo => CurrencyType.FromCodigo(codigo!));
        });
        
        //Foregin Key's
        builder.HasOne<Loan> ().WithMany().HasForeignKey(Installment => Installment.LoanId);
    
    }   
}