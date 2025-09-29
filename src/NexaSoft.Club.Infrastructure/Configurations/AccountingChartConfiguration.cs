using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class AccountingChartConfiguration : IEntityTypeConfiguration<AccountingChart>
{
    public void Configure(EntityTypeBuilder<AccountingChart> builder)
    {
        builder.ToTable("accounting_charts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountCode)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.AccountName)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.AccountType)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.ParentAccountId)
            .IsRequired(false);


        builder.HasOne(x => x.ParentAccount)
               .WithMany()
               .HasForeignKey(x => x.ParentAccountId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.AllowsTransactions)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.StateAccountingChart)
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

        builder.HasIndex(x => x.AccountCode)
            .HasDatabaseName("ix_accountingchart_accountcode");

        builder.HasIndex(x => x.AccountType)
            .HasDatabaseName("ix_accountingchart_accounttype");

        builder.HasIndex(x => x.Level)
            .HasDatabaseName("ix_accountingchart_level");

        builder.HasIndex(x => x.AllowsTransactions)
            .HasDatabaseName("ix_accountingchart_allowstransactions");

    }
}
