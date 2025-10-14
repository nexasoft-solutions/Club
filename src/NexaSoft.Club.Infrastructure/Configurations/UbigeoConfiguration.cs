using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class UbigeoConfiguration : IEntityTypeConfiguration<Ubigeo>
{
    public void Configure(EntityTypeBuilder<Ubigeo> builder)
    {
        builder.ToTable("ubigeos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Description)
            .HasMaxLength(180)
            .IsRequired(false);

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.ParentId)
            .IsRequired(false);

        builder.Property(x => x.StateUbigeo)
            .IsRequired();

        builder.HasOne(x => x.Parent)
                .WithMany()
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.HasIndex(x => new { x.Level, x.Description })
         .HasDatabaseName("ix_ubigeo_level_description");


        builder.HasIndex(x => x.Description)
          .HasDatabaseName("ix_ubigeo_description");

        builder.HasIndex(x => x.Level)
            .HasDatabaseName("ix_ubigeo_level");



    }
}
