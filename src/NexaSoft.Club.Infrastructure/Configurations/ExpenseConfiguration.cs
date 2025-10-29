using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.Expenses;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("expenses");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CostCenterId)
            .IsRequired(false);


         builder.HasOne(x => x.CostCenter)
                .WithMany()
                .HasForeignKey(x => x.CostCenterId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.ExpenseDate)
            .IsRequired(false);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.DocumentNumber)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.DocumentPath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.StateExpense)
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

        builder.HasIndex(x => x.CostCenterId)
            .HasDatabaseName("ix_expense_costcenterid");

    }
}
