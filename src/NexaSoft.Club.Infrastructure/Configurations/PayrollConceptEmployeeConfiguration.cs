using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollConceptEmployeeConfiguration : IEntityTypeConfiguration<PayrollConceptEmployee>
{
    public void Configure(EntityTypeBuilder<PayrollConceptEmployee> builder)
    {
        builder.ToTable("payroll_concept_employees");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PayrollConceptId)
            .IsRequired(false);


         builder.HasOne(x => x.PayrollConcept)
                .WithMany()
                .HasForeignKey(x => x.PayrollConceptId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EmployeeId)
            .IsRequired(false);


         builder.HasOne(x => x.EmployeeInfo)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollConceptEmployee)
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

        builder.HasIndex(x => x.PayrollConceptId)
            .HasDatabaseName("ix_payrollconceptemployee_payrollconceptid");

        builder.HasIndex(x => x.EmployeeId)
            .HasDatabaseName("ix_payrollconceptemployee_employeeid");

    }
}
