using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class EstudioAmbientalConfiguration : IEntityTypeConfiguration<EstudioAmbiental>
{
    public void Configure(EntityTypeBuilder<EstudioAmbiental> builder)
    {
        builder.ToTable("estudios_ambientales");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Proyecto)
            .HasMaxLength(220)
            .IsRequired(true);

        builder.Property(x => x.CodigoEstudio)
             .HasMaxLength(16)
             .IsRequired(true);

        builder.Property(x => x.FechaInicio)
            .IsRequired();

        builder.Property(x => x.FechaFin)
            .IsRequired();

        builder.Property(x => x.Detalles)
            .IsRequired(false);

        builder.Property(x => x.EmpresaId)
            .IsRequired();

        builder.Property(x => x.EstadoEstudioAmbiental)
            .IsRequired();


        builder.HasOne(x => x.Empresa)
               .WithMany()
               .HasForeignKey(x => x.EmpresaId)
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

        /*builder.HasMany(e => e.Capitulos)
               .WithOne(c => c.EstudioAmbiental)
               .HasForeignKey(c => c.EstudioAmbientalId);*/


        builder.HasIndex(x => x.Proyecto)
            .HasDatabaseName("ix_estudioambiental_proyecto");

        builder.HasIndex(x => x.CodigoEstudio)
            .HasDatabaseName("ix_estudioambiental_codigoestudio");

    }
}
