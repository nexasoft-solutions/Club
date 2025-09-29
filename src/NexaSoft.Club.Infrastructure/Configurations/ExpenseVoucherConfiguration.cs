using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class ExpenseVoucherConfiguration : IEntityTypeConfiguration<ExpenseVoucher>
{
    public void Configure(EntityTypeBuilder<ExpenseVoucher> builder)
    {
        builder.ToTable("expenses_vouchers");
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

        builder.Property(x => x.VoucherNumber)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.SupplierName)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.IssueDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.ExpenseAccountId)
            .IsRequired();


         builder.HasOne(x => x.ExpenseAccount)
                .WithMany()
                .HasForeignKey(x => x.ExpenseAccountId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StateExpenseVoucher)
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

        builder.HasIndex(x => x.VoucherNumber)
            .HasDatabaseName("ix_expensevoucher_vouchernumber");

        builder.HasIndex(x => x.IssueDate)
            .HasDatabaseName("ix_expensevoucher_issuedate");

    }
}
