using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.SpaceTypes;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SpaceTypeConfiguration : IEntityTypeConfiguration<SpaceType>
{
    public void Configure(EntityTypeBuilder<SpaceType> builder)
    {
        builder.ToTable("space_types");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.StateSpaceType)
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

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_spacetype_name");

    }
}
