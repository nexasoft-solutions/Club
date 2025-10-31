using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollConceptConfiguration : IEntityTypeConfiguration<PayrollConcept>
{
    public void Configure(EntityTypeBuilder<PayrollConcept> builder)
    {
        builder.ToTable("payroll_concepts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.ConceptTypePayrollId)
            .IsRequired(false);


        builder.HasOne(x => x.ConceptTypePayroll)
               .WithMany()
               .HasForeignKey(x => x.ConceptTypePayrollId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PayrollFormulaId)
            .IsRequired(false);


        builder.HasOne(x => x.PayrollFormula)
               .WithMany()
               .HasForeignKey(x => x.PayrollFormulaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ConceptCalculationTypeId)
            .IsRequired(false);


        builder.HasOne(x => x.ConceptCalculationType)
               .WithMany()
               .HasForeignKey(x => x.ConceptCalculationTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.FixedValue)
            .IsRequired(false);

        builder.Property(x => x.PorcentajeValue)
            .IsRequired(false);

        builder.Property(x => x.ConceptApplicationTypesId)
            .IsRequired(false);


        builder.HasOne(x => x.ConceptApplicationType)
               .WithMany()
               .HasForeignKey(x => x.ConceptApplicationTypesId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.AccountingChartId)
            .IsRequired(false);


        builder.HasOne(x => x.AccountingChart)
               .WithMany()
               .HasForeignKey(x => x.AccountingChartId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PayrollTypeId)
            .IsRequired(false);

        builder.HasOne(x => x.PayrollType)
                .WithMany()
                .HasForeignKey(x => x.PayrollTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollConcept)
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
            .HasDatabaseName("ix_payrollconcept_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_payrollconcept_name");

        builder.HasIndex(x => x.ConceptTypePayrollId)
            .HasDatabaseName("ix_payrollconcept_concepttypepayrollid");

        builder.HasIndex(x => x.PayrollFormulaId)
            .HasDatabaseName("ix_payrollconcept_payrollformulaid");

        builder.HasIndex(x => x.ConceptCalculationTypeId)
            .HasDatabaseName("ix_payrollconcept_conceptcalculationtypeid");

        builder.HasIndex(x => x.AccountingChartId)
            .HasDatabaseName("ix_payrollconcept_accountingchartid");

        builder.HasIndex(x => x.PayrollTypeId)
            .HasDatabaseName("ix_payrollconcept_payrolltypeid");

        builder.HasIndex(x => x.ConceptApplicationTypesId)
            .HasDatabaseName("ix_payrollconcept_conceptapplicationtypesid");

    }
}
