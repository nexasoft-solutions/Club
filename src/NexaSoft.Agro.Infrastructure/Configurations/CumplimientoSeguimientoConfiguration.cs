using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class CumplimientoSeguimientoConfiguration : IEntityTypeConfiguration<CumplimientoSeguimiento>
{
  public void Configure(EntityTypeBuilder<CumplimientoSeguimiento> builder)
    {
        builder.ToTable("cumplimientos_seguimientos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EstadoAnteriorId)
            .IsRequired(false);

        builder.Property(x => x.EstadoNuevoId)
            .IsRequired();

        builder.Property(x => x.Observaciones)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.FechaCambio)
            .IsRequired();


        builder.HasOne(x => x.EventoRegulatorio)
               .WithMany()
               .HasForeignKey(x => x.EventoRegulatorioId)
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

        builder.HasIndex(x => x.FechaCambio)
            .HasDatabaseName("ix_cumplimiento_segumiento_fechacambio");

    }
}
