using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanManager.Domain.Installments;
using LoanManager.Domain.Payments;
using LoanManager.Domain.Shared;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");
        builder.HasKey(Payment => Payment.Id);

        builder.Property(Payment => Payment.Id)
        .HasConversion(PaymentId => PaymentId.Value, Value => new PaymentId(Value));

        builder.OwnsOne(Payment => Payment.Amount, capitalBuilder =>
        {
            capitalBuilder.Property(amount => amount.CurrencyType)
            .HasConversion(CurrencyType => CurrencyType.Code, code => CurrencyType.FromCodigo(code!));
        });

        builder.OwnsOne(Payment => Payment.Surcharge, capitalBuilder =>
        {
            capitalBuilder.Property(amount => amount.CurrencyType)
            .HasConversion(CurrencyType => CurrencyType.Code, code => CurrencyType.FromCodigo(code!));
        });

          builder.HasOne<Installment> ().WithMany().HasForeignKey(payment => payment.InstallmentId);
    }
}