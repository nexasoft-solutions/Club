using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class IncomeTaxScaleConfiguration : IEntityTypeConfiguration<IncomeTaxScale>
{
    public void Configure(EntityTypeBuilder<IncomeTaxScale> builder)
    {
        builder.ToTable("income_tax_scales");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ScaleYear)
            .IsRequired();

        builder.Property(x => x.MinIncome)
            .IsRequired();

        builder.Property(x => x.MaxIncome)
            .IsRequired(false);

        builder.Property(x => x.FixedAmount)
            .IsRequired();

        builder.Property(x => x.Rate)
            .IsRequired();

        builder.Property(x => x.ExcessOver)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.StateIncomeTaxScale)
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

        builder.HasIndex(x => x.ScaleYear)
            .HasDatabaseName("ix_incometaxscale_scaleyear");

    }
}
