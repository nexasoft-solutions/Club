using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SpecialRateConfiguration : IEntityTypeConfiguration<SpecialRate>
{
    public void Configure(EntityTypeBuilder<SpecialRate> builder)
    {
        builder.ToTable("special_rates");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RateTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.RateType)
                .WithMany()
                .HasForeignKey(x => x.RateTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Multiplier)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired(false);

        builder.Property(x => x.EndTime)
            .IsRequired(false);

        builder.Property(x => x.StateSpecialRate)
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

        builder.HasIndex(x => x.RateTypeId)
            .HasDatabaseName("ix_specialrate_ratetypeid");

    }
}
