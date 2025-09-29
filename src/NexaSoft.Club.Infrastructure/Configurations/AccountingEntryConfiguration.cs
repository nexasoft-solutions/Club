using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class AccountingEntryConfiguration : IEntityTypeConfiguration<AccountingEntry>
{
    public void Configure(EntityTypeBuilder<AccountingEntry> builder)
    {
        builder.ToTable("accounting_entries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EntryNumber)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.EntryDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.SourceModule)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.SourceId)
            .IsRequired(false);

        builder.Property(x => x.TotalDebit)
            .IsRequired();

        builder.Property(x => x.TotalCredit)
            .IsRequired();

        builder.Property(x => x.IsAdjusted)
            .IsRequired();

        builder.Property(x => x.AdjustmentReason)
            .IsRequired(false);

        builder.Property(x => x.StateAccountingEntry)
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

        builder.HasIndex(x => x.EntryNumber)
            .HasDatabaseName("ix_accountingentry_entrynumber");

        builder.HasIndex(x => x.EntryDate)
            .HasDatabaseName("ix_accountingentry_entrydate");

        builder.HasIndex(x => x.SourceModule)
            .HasDatabaseName("ix_accountingentry_sourcemodule");

        builder.HasIndex(x => new { x.SourceModule, x.SourceId })
            .HasDatabaseName("idx_accounting_entries_source");

    }
}
