using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.RateTypes;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class RateTypeConfiguration : IEntityTypeConfiguration<RateType>
{
    public void Configure(EntityTypeBuilder<RateType> builder)
    {
        builder.ToTable("rate_types");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.StateRateType)
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
            .HasDatabaseName("ix_ratetype_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_ratetype_name");

    }
}
