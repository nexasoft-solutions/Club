using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SpaceConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.ToTable("spaces");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SpaceName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.SpaceTypeId)
            .IsRequired();

        builder.Property(x => x.Capacity)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.StandardRate)
            .IsRequired();


        builder.Property(x => x.RequiresApproval)
            .IsRequired();

        builder.Property(x => x.MaxReservationHours)
            .IsRequired();

        builder.Property(x => x.IncomeAccountId)
            .IsRequired(false);

        builder.HasOne(x => x.SpaceType)
                .WithMany()
                .HasForeignKey(x => x.SpaceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(x => x.IncomeAccount)
                .WithMany()
                .HasForeignKey(x => x.IncomeAccountId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StateSpace)
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

        builder.HasIndex(x => x.SpaceName)
            .HasDatabaseName("ix_space_spacename");

        builder.HasIndex(x => x.SpaceTypeId)
            .HasDatabaseName("ix_space_spacetype");

    }
}
