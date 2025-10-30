using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollConceptEmployeeTypeConfiguration : IEntityTypeConfiguration<PayrollConceptEmployeeType>
{
    public void Configure(EntityTypeBuilder<PayrollConceptEmployeeType> builder)
    {
        builder.ToTable("payroll_concept_employee_types");
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

        builder.Property(x => x.EmployeeTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.EmployeeType)
                .WithMany()
                .HasForeignKey(x => x.EmployeeTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollConceptEmployeeType)
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
            .HasDatabaseName("ix_payrollconceptemployeetype_payrollconceptid");

        builder.HasIndex(x => x.EmployeeTypeId)
            .HasDatabaseName("ix_payrollconceptemployeetype_employeetypeid");

    }
}
