using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.SpacePhotos;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SpacePhotoConfiguration : IEntityTypeConfiguration<SpacePhoto>
{
    public void Configure(EntityTypeBuilder<SpacePhoto> builder)
    {
        builder.ToTable("space_photos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SpaceId)
            .IsRequired();


         builder.HasOne(x => x.Space)
                .WithMany(x => x.SpacePhotos)
                .HasForeignKey(x => x.SpaceId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.StateSpacePhoto)
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
            .HasDatabaseName("ix_spacephoto_spaceid");

    }
}
