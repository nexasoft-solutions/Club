using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollPeriodStatusConfiguration : IEntityTypeConfiguration<PayrollPeriodStatus>
{
    public void Configure(EntityTypeBuilder<PayrollPeriodStatus> builder)
    {
        builder.ToTable("payroll_period_statuses");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.StatePayrollPeriodStatus)
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
            .HasDatabaseName("ix_payrollperiod_status_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_payrollperiod_status_name");

    }
}
