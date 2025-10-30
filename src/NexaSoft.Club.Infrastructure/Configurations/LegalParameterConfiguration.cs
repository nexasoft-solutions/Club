using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class LegalParameterConfiguration : IEntityTypeConfiguration<LegalParameter>
{
    public void Configure(EntityTypeBuilder<LegalParameter> builder)
    {
        builder.ToTable("legal_parameters");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.ValueText)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.EffectiveDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.Category)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.StateLegalParameter)
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
            .HasDatabaseName("ix_legalparameter_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_legalparameter_name");

        builder.HasIndex(x => new { x.EffectiveDate, x.EndDate })
            .HasDatabaseName("ix_legal_parameters_dates");

        builder.HasIndex(x => x.Category)
            .HasDatabaseName("ix_legalparameter_category");

    }
}
