using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class CumplimientoConfiguration : IEntityTypeConfiguration<Cumplimiento>
{
    public void Configure(EntityTypeBuilder<Cumplimiento> builder)
    {
        builder.ToTable("cumplimientos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FechaCumplimiento)
            .IsRequired(false);

        builder.Property(x => x.RegistradoaTiempo)
            .IsRequired(false);

        builder.Property(x => x.Observaciones)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.EventoRegulatorioId)
            .IsRequired();


        builder.HasOne(x => x.EventoRegulatorio)
               .WithMany()
               .HasForeignKey(x => x.EventoRegulatorioId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EstadoCumplimiento)
            .IsRequired();

        builder.Property(x => x.UsuarioCreacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioModificacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioEliminacion)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.HasIndex(x => x.FechaCumplimiento)
            .HasDatabaseName("ix_cumplimiento_fechacumplimiento");

        builder.HasIndex(x => x.RegistradoaTiempo)
            .HasDatabaseName("ix_cumplimiento_registradoatiempo");

        builder.HasIndex(x => x.EventoRegulatorioId)
            .HasDatabaseName("ix_cumplimiento_eventoregulatorioid");

    }
}
