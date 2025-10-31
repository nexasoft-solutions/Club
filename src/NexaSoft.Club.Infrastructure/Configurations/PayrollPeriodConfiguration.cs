using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollPeriodConfiguration : IEntityTypeConfiguration<PayrollPeriod>
{
    public void Configure(EntityTypeBuilder<PayrollPeriod> builder)
    {
        builder.ToTable("payroll_periods");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PeriodName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.StartDate)
            .IsRequired(false);

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.TotalAmount)
            .IsRequired();

        builder.Property(x => x.TotalEmployees)
            .IsRequired(false);

        builder.Property(x => x.StatusId)
            .IsRequired(false);

        builder.Property(x => x.PayrollTypeId)
            .IsRequired(false);    

        builder.HasOne(x => x.PayrollType)
               .WithMany()
               .HasForeignKey(x => x.PayrollTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollPeriod)
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

        builder.HasIndex(x => x.PeriodName)
            .HasDatabaseName("ix_payrollperiod_periodname");
            
        builder.HasIndex(x => x.PayrollTypeId)
            .HasDatabaseName("ix_payrollperiod_payrolltypeid");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_payrollperiod_statusid");

    }
}
