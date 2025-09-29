using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class AccountingEntryItemConfiguration: IEntityTypeConfiguration<AccountingEntryItem>
{
    public void Configure(EntityTypeBuilder<AccountingEntryItem> builder)
    {
        builder.ToTable("accounting_entry_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountingEntryId)
            .IsRequired();

        builder.Property(x => x.AccountingChartId)
            .IsRequired();

        builder.Property(x => x.DebitAmount)
            .IsRequired();

        builder.Property(x => x.CreditAmount)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.StateEntryItem)
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

        
        // Relaciones (FK)
        builder.HasOne(x => x.AccountingEntry)
            .WithMany() // o .WithMany(a => a.Items) si tenés navegación inversa
            .HasForeignKey(x => x.AccountingEntryId)
            .HasConstraintName("fk_accounting_entry_items_accounting_entries")
            .OnDelete(DeleteBehavior.Restrict); // O .Cascade si corresponde

        builder.HasOne(x => x.AccountingChart)
            .WithMany() // o .WithMany(c => c.Items) si tenés navegación inversa
            .HasForeignKey(x => x.AccountingChartId)
            .HasConstraintName("fk_accounting_entry_items_accounting_charts")
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(x => x.AccountingEntryId)
            .HasDatabaseName("ix_accounting_entry_items_entry_id");

        builder.HasIndex(x => x.AccountingChartId)
            .HasDatabaseName("ix_accounting_entry_items_chart_id");

        // Checks (opcional, pero EF Core 7+ soporta check constraints)
        /*builder.HasCheckConstraint("chk_debit_credit_not_both", "NOT (debit_amount > 0 AND credit_amount > 0)");
        builder.HasCheckConstraint("chk_positive_amounts", "debit_amount >= 0 AND credit_amount >= 0");*/
    }
}
