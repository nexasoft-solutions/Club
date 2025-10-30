using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollConceptDepartmentConfiguration : IEntityTypeConfiguration<PayrollConceptDepartment>
{
    public void Configure(EntityTypeBuilder<PayrollConceptDepartment> builder)
    {
        builder.ToTable("payroll_concept_departments");
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

        builder.Property(x => x.DepartmentId)
            .IsRequired(false);


         builder.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollConceptDepartment)
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
            .HasDatabaseName("ix_payrollconceptdepartment_payrollconceptid");

        builder.HasIndex(x => x.DepartmentId)
            .HasDatabaseName("ix_payrollconceptdepartment_departmentid");

    }
}
