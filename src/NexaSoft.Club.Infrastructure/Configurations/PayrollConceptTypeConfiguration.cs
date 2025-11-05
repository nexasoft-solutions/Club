using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollConceptTypeConfiguration: IEntityTypeConfiguration<PayrollConceptType>
{
    public void Configure(EntityTypeBuilder<PayrollConceptType> builder)
    {
        builder.ToTable("payroll_concept_types");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
      

        builder.Property(x => x.PayrollConceptId)
            .IsRequired();


        builder.HasOne(x => x.PayrollConcept)
               .WithMany()
               .HasForeignKey(x => x.PayrollConceptId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PayrollTypeId)
            .IsRequired();


        builder.HasOne(x => x.PayrollType)
               .WithMany()
               .HasForeignKey(x => x.PayrollTypeId)
               .OnDelete(DeleteBehavior.Restrict);


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
            .HasDatabaseName("ix_payrollconcept_payrollconceptid");

        builder.HasIndex(x => x.PayrollTypeId)
            .HasDatabaseName("ix_payrollconcept_payrolltypeid");

        builder.HasIndex(x => new { x.PayrollConceptId, x.PayrollTypeId })
            .HasDatabaseName("ux_payrollconcepttype_concept_type");

    }
}

