using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollConfigConfiguration : IEntityTypeConfiguration<PayrollConfig>
{
    public void Configure(EntityTypeBuilder<PayrollConfig> builder)
    {
        builder.ToTable("payroll_configs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
            .IsRequired(false);


         builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PayPeriodTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.PayPeriodType)
                .WithMany()
                .HasForeignKey(x => x.PayPeriodTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RegularHoursPerDay)
            .IsRequired();

        builder.Property(x => x.WorkDaysPerWeek)
            .IsRequired();

        builder.Property(x => x.StatePayrollConfig)
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

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_payrollconfig_companyid");

        builder.HasIndex(x => x.PayPeriodTypeId)
            .HasDatabaseName("ix_payrollconfig_payperiodtypeid");

    }
}
