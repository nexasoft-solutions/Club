using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollFormulaConfiguration : IEntityTypeConfiguration<PayrollFormula>
{
    public void Configure(EntityTypeBuilder<PayrollFormula> builder)
    {
        builder.ToTable("payroll_formulas");
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

        builder.Property(x => x.FormulaExpression)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(x => x.Variables)
            .HasColumnType("jsonb")
            .IsRequired(false);

        builder.Property(x => x.StatePayrollFormula)
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
            .HasDatabaseName("ix_payrollformula_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_payrollformula_name");

        builder.HasIndex(x => x.FormulaExpression)
            .HasDatabaseName("ix_payrollformula_formulaexpression");

    }
}
