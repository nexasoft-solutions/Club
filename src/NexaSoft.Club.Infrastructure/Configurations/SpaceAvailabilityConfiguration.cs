using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SpaceAvailabilityConfiguration : IEntityTypeConfiguration<SpaceAvailability>
{
    public void Configure(EntityTypeBuilder<SpaceAvailability> builder)
    {
        builder.ToTable("space_availabilities");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SpaceId)
            .IsRequired();


         builder.HasOne(x => x.Space)
                .WithMany()
                .HasForeignKey(x => x.SpaceId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DayOfWeek)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();


        builder.Property(x => x.StateSpaceAvailability)
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

        builder.HasIndex(x => x.SpaceId)
            .HasDatabaseName("ix_spaceavailability_spaceid");

    }
}
