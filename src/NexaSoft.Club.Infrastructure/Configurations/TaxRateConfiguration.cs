using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.TaxRates;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class TaxRateConfiguration : IEntityTypeConfiguration<TaxRate>
{
    public void Configure(EntityTypeBuilder<TaxRate> builder)
    {
        builder.ToTable("tax_rates");
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

        builder.Property(x => x.RateValue)
            .IsRequired();

        builder.Property(x => x.RateType)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.MinAmount)
            .IsRequired(false);

        builder.Property(x => x.MaxAmount)
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

        builder.Property(x => x.AppliesTo)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.StateTaxRate)
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
            .HasDatabaseName("ix_taxrate_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_taxrate_name");

        builder.HasIndex(x => x.Category)
            .HasDatabaseName("ix_taxrate_category");

    }
}
