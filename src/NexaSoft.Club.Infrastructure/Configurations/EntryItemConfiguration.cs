using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.EntryItems;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class EntryItemConfiguration : IEntityTypeConfiguration<EntryItem>
{
    public void Configure(EntityTypeBuilder<EntryItem> builder)
    {
        builder.ToTable("entry_items");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountingEntryId)
            .IsRequired();


         builder.HasOne(x => x.AccountingEntry)
                .WithMany()
                .HasForeignKey(x => x.AccountingEntryId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.AccountingChartId)
            .IsRequired();


         builder.HasOne(x => x.AccountingChart)
                .WithMany()
                .HasForeignKey(x => x.AccountingChartId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DebitAmount)
            .IsRequired();

        builder.Property(x => x.CreditAmount)
            .IsRequired();

        builder.Property(x => x.Description)
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

        builder.HasIndex(x => x.AccountingEntryId)
            .HasDatabaseName("ix_entryitem_entryid");

        builder.HasIndex(x => x.AccountingChartId)
            .HasDatabaseName("ix_entryitem_accountid");

    }
}
