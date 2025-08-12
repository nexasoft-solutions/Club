using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class UbigeoConfiguration : IEntityTypeConfiguration<Ubigeo>
{
    public void Configure(EntityTypeBuilder<Ubigeo> builder)
    {
        builder.ToTable("ubigeos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(180)
            .IsRequired(false);

        builder.Property(x => x.Nivel)
            .IsRequired();

        builder.Property(x => x.PadreId)
            .IsRequired(false);

        builder.Property(x => x.EstadoUbigeo)
            .IsRequired();

        builder.HasOne(x => x.Padre)
                .WithMany()
                .HasForeignKey(x => x.PadreId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Descripcion)
            .HasDatabaseName("ix_ubigeo_descripcion");

        builder.HasIndex(x => x.Nivel)
            .HasDatabaseName("ix_ubigeo_nivel");

        builder.HasIndex(x => new { x.Nivel, x.Descripcion })
         .HasDatabaseName("ix_ubigeo_nivel_descripcion");


    }
}
