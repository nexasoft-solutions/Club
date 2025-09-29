using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SpaceRateConfiguration : IEntityTypeConfiguration<SpaceRate>
{
    public void Configure(EntityTypeBuilder<SpaceRate> builder)
    {
        builder.ToTable("space_rates");
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

        builder.Property(x => x.MemberTypeId)
            .IsRequired();


         builder.HasOne(x => x.MemberType)
                .WithMany()
                .HasForeignKey(x => x.MemberTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Rate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.StateSpaceRate)
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
            .HasDatabaseName("ix_spacerate_spaceid");

        builder.HasIndex(x => x.MemberTypeId)
            .HasDatabaseName("ix_spacerate_membertypeid");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_spacerate_isactive");

    }
}
