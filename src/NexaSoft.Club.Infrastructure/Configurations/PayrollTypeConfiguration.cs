using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollTypeConfiguration : IEntityTypeConfiguration<PayrollType>
{
    public void Configure(EntityTypeBuilder<PayrollType> builder)
    {
        builder.ToTable("payroll_types");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.StatePayrollType)
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

        builder.HasIndex(x => x.Code)
            .HasDatabaseName("ix_payrolltype_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_payrolltype_name");

    }
}
