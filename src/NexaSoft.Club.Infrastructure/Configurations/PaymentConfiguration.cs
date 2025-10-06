using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MemberId)
            .IsRequired();


        builder.HasOne(x => x.Member)
               .WithMany()
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

    
        builder.Property(x => x.TotalAmount)
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();

        builder.Property(x => x.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.ReferenceNumber)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.ReceiptNumber)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.IsPartial)
            .IsRequired();

         builder.Property(x => x.Status)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.AccountingEntryId)
            .IsRequired(false);


        builder.HasOne(x => x.AccountingEntry)
               .WithMany()
               .HasForeignKey(x => x.AccountingEntryId)
               .OnDelete(DeleteBehavior.Restrict);

        
        // Relación con PaymentItems (uno a muchos)
        builder.HasMany(x => x.PaymentItems)
               .WithOne(x => x.Payment)
               .HasForeignKey(x => x.PaymentId)
               .OnDelete(DeleteBehavior.Cascade); // Si se elimina el pago, se eliminan los items

        builder.Property(x => x.StatePayment)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(40)
            .IsRequired(false);

         builder.HasIndex(x => x.MemberId)
            .HasDatabaseName("ix_payment_memberid");

        builder.HasIndex(x => x.PaymentDate)
            .HasDatabaseName("ix_payment_paymentdate");

        builder.HasIndex(x => x.PaymentMethod)
            .HasDatabaseName("ix_payment_paymentmethod");

        builder.HasIndex(x => x.ReceiptNumber)
            .HasDatabaseName("ix_payment_receiptnumber")
            .IsUnique(); // Número de recibo único

        builder.HasIndex(x => x.AccountingEntryId)
            .HasDatabaseName("ix_payment_accountingentryid");

        builder.HasIndex(x => new { x.MemberId, x.PaymentDate })
            .HasDatabaseName("idx_payments_member_date");

        // Índice para búsquedas por estado
        builder.HasIndex(x => x.StatePayment)
            .HasDatabaseName("ix_payment_state");

    }
}
