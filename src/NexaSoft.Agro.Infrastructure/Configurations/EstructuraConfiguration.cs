using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class EstructuraConfiguration : IEntityTypeConfiguration<Estructura>
{
    public void Configure(EntityTypeBuilder<Estructura> builder)
    {
        builder.ToTable("estructuras");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TipoEstructuraId)
            .IsRequired();

        builder.Property(x => x.NombreEstructura)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.DescripcionEstructura)
            .HasMaxLength(550)
            .IsRequired();

        builder.Property(x => x.PadreEstructuraId)
              .IsRequired(false);

        builder.Property(x => x.SubCapituloId)
            .IsRequired();

        builder.Property(x => x.EstadoEstructura)
            .IsRequired();


        builder.HasOne(e => e.Padre)
            .WithMany(e => e.Hijos)
            .HasForeignKey(e => e.PadreEstructuraId)
            .OnDelete(DeleteBehavior.Restrict);


        /*builder.HasMany(e => e.Archivos)
            .WithOne(a => a.Estructura)
            .HasForeignKey(a => a.EstructuraId);*/

        builder.HasOne(x => x.SubCapitulo)
               .WithMany()
               .HasForeignKey(x => x.SubCapituloId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.UsuarioCreacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioModificacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioEliminacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.HasIndex(x => x.PadreEstructuraId)
         .HasDatabaseName("ix_estructura_padreestructuraid");

        builder.HasIndex(x => x.SubCapituloId)
            .HasDatabaseName("ix_estructura_subcapituloid");

    }
}
