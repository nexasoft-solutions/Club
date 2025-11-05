using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollDetailConfiguration: IEntityTypeConfiguration<PayrollDetail>
{
    public void Configure(EntityTypeBuilder<PayrollDetail> builder)
    {
        builder.ToTable("payroll_details");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PayrollPeriodId)
            .IsRequired();

        builder.Property(x => x.EmployeeId)
            .IsRequired();

        builder.Property(x => x.CostCenterId)
            .IsRequired(false);

        builder.Property(x => x.BaseSalary)
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

        builder.Property(x => x.TotalIncome)
            .HasColumnType("decimal(12,2)")
            .IsRequired(false);

        builder.Property(x => x.TotalDeductions)
            .HasColumnType("decimal(12,2)")
            .IsRequired(false);

        builder.Property(x => x.NetPay)
            .HasColumnType("decimal(12,2)")
            .IsRequired(false);

        builder.Property(x => x.StatusId)
            .IsRequired(false);

        builder.Property(x => x.CalculatedAt)
            .IsRequired(false);

        builder.Property(x => x.PaidAt)
            .IsRequired(false);

        // Relaciones
        builder.HasOne(x => x.PayrollPeriod)
            .WithMany(d => d.PayrollDetails)
            .HasForeignKey(x => x.PayrollPeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CostCenter)
            .WithMany()
            .HasForeignKey(x => x.CostCenterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollDetail)
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

        // Índices
        builder.HasIndex(x => x.PayrollPeriodId)
            .HasDatabaseName("ix_payrolldetail_payrollperiodid");

        builder.HasIndex(x => x.EmployeeId)
            .HasDatabaseName("ix_payrolldetail_employeeid");

        builder.HasIndex(x => x.CostCenterId)
            .HasDatabaseName("ix_payrolldetail_costcenterid");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_payrolldetail_statusid");
    }
}