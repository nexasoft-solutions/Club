using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PaymentItemConfiguration: IEntityTypeConfiguration<PaymentItem>
{
    public void Configure(EntityTypeBuilder<PaymentItem> builder)
    {
        builder.ToTable("payment_items");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        // Relación con Payment (obligatoria)
        builder.Property(x => x.PaymentId)
            .HasColumnName("payment_id")
            .IsRequired();

        builder.HasOne(x => x.Payment)
               .WithMany(x => x.PaymentItems)
               .HasForeignKey(x => x.PaymentId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relación con MemberFee (obligatoria)
        builder.Property(x => x.MemberFeeId)
            .HasColumnName("member_fee_id")
            .IsRequired();

        builder.HasOne(x => x.MemberFee)
               .WithMany()
               .HasForeignKey(x => x.MemberFeeId)
               .OnDelete(DeleteBehavior.Restrict);

        // Monto del item
        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.StatePaymentItem)
            .HasColumnName("state_payment_item")
            .IsRequired();

        // Campos de auditoría
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasColumnName("deleted_by")
            .HasMaxLength(40)
            .IsRequired(false);

        // Índices
        builder.HasIndex(x => x.PaymentId)
            .HasDatabaseName("ix_paymentitem_paymentid");

        builder.HasIndex(x => x.MemberFeeId)
            .HasDatabaseName("ix_paymentitem_memberfeeid");

        builder.HasIndex(x => new { x.PaymentId, x.MemberFeeId })
            .HasDatabaseName("idx_paymentitems_payment_fee")
            .IsUnique() // Evitar duplicados de la misma cuota en el mismo pago
            .HasFilter("deleted_at IS NULL"); // Solo considerar registros activos

        builder.HasIndex(x => x.StatePaymentItem)
            .HasDatabaseName("ix_paymentitem_state");

        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName("ix_paymentitem_createdat");

        // Filtro para soft delete
        builder.HasQueryFilter(x => x.DeletedAt == null);

        // Check constraint para monto positivo
        builder.ToTable(t => t.HasCheckConstraint("chk_paymentitem_amount_positive", "amount > 0"));
    }
}