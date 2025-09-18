using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class EventoRegulatorioResponsableConfiguration : IEntityTypeConfiguration<EventoRegulatorioResponsable>
{
    public void Configure(EntityTypeBuilder<EventoRegulatorioResponsable> builder)
    {
        builder.ToTable("eventos_regulatorios_responsables");

        // Clave compuesta porque es tabla puente
        builder.HasKey(x => new { x.EventoRegulatorioId, x.ResponsableId });

        builder.Property(x => x.EventoRegulatorioId)
            .HasColumnName("evento_regulatorio_id")
            .IsRequired();

        builder.Property(x => x.ResponsableId)
            .HasColumnName("responsable_id")
            .IsRequired();

        builder.Property(x => x.UsuarioCreacion)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.FechaCreacion)
            .IsRequired();

        // Opcional: si quieres configurar navegación inversa (normalmente no es obligatorio)
        // builder.HasOne<EventoRegulatorio>()
        //        .WithMany("_responsablesSecundarios")
        //        .HasForeignKey(x => x.EventoRegulatorioId)
        //        .OnDelete(DeleteBehavior.Cascade);

        // Si tienes la entidad Responsable, también puedes configurar aquí.
    }
}