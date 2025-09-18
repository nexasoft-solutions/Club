using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class EventoRegulatorioConfiguration : IEntityTypeConfiguration<EventoRegulatorio>
{
    public void Configure(EntityTypeBuilder<EventoRegulatorio> builder)
    {
        builder.ToTable("eventos_regulatorios");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreEvento)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.TipoEventoId)
            .IsRequired();

        builder.Property(x => x.FrecuenciaEventoId)
            .IsRequired();

        builder.Property(x => x.FechaExpedición)
            .IsRequired(false);

        builder.Property(x => x.FechaVencimiento)
            .IsRequired(false);

        builder.Property(x => x.Descripcion)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.NotificarDíasAntes)
            .IsRequired();

        builder.Property(x => x.ResponsableId)
            .IsRequired();


        builder.HasOne(x => x.Responsable)
               .WithMany()
               .HasForeignKey(x => x.ResponsableId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EstadoEventoId)
            .IsRequired();

        builder.Property(x => x.EstudioAmbientalId)
            .IsRequired();


        builder.HasOne(x => x.EstudioAmbiental)
               .WithMany()
               .HasForeignKey(x => x.EstudioAmbientalId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EstadoEventoRegulatorio)
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

        builder.HasMany(x => x.Responsables)
            .WithOne()
            .HasForeignKey(x => x.EventoRegulatorioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.NombreEvento)
            .HasDatabaseName("ix_eventoregulatorio_nombreevento");

        builder.HasIndex(x => x.TipoEventoId)
            .HasDatabaseName("ix_eventoregulatorio_tipoeventoid");

        builder.HasIndex(x => x.FrecuenciaEventoId)
            .HasDatabaseName("ix_eventoregulatorio_fecuenciaeventoid");

        builder.HasIndex(x => x.FechaExpedición)
            .HasDatabaseName("ix_eventoregulatorio_fechaexpedición");

        builder.HasIndex(x => x.FechaVencimiento)
            .HasDatabaseName("ix_eventoregulatorio_fechavencimiento");

        builder.HasIndex(x => x.ResponsableId)
            .HasDatabaseName("ix_eventoregulatorio_responsableid");

        builder.HasIndex(x => x.EstudioAmbientalId)
            .HasDatabaseName("ix_eventoregulatorio_estudioambientalid");

    }
}
