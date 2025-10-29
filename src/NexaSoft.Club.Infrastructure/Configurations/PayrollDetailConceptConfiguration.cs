using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollDetailConceptConfiguration: IEntityTypeConfiguration<PayrollDetailConcept>
{
    public void Configure(EntityTypeBuilder<PayrollDetailConcept> builder)
    {
        builder.ToTable("payroll_detail_concepts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PayrollDetailId)
            .IsRequired();

        builder.Property(x => x.ConceptId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

        builder.Property(x => x.Quantity)
            .HasColumnType("decimal(8,2)")
            .IsRequired(false);

        builder.Property(x => x.CalculatedValue)
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired(false);

        // Relaciones
        builder.HasOne(x => x.PayrollDetail)
            .WithMany()
            .HasForeignKey(x => x.PayrollDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Concept)
            .WithMany()
            .HasForeignKey(x => x.ConceptId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StatePayrollDetailConcept)
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
        builder.HasIndex(x => x.PayrollDetailId)
            .HasDatabaseName("ix_payrolldetailconcept_payrolldetailid");

        builder.HasIndex(x => x.ConceptId)
            .HasDatabaseName("ix_payrolldetailconcept_conceptid");
    }
}
