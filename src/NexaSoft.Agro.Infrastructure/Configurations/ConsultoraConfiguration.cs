using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Consultoras;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class ConsultoraConfiguration : IEntityTypeConfiguration<Consultora>
{
    public void Configure(EntityTypeBuilder<Consultora> builder)
    {
        builder.ToTable("consultoras");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreConsultora)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.DireccionConsultora)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.RepresentanteConsultora)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.RucConsultora)
            .HasMaxLength(11)
            .IsRequired(false);

        builder.Property(x => x.CorreoOrganizacional)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.EstadoConsultora)
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

        builder.HasIndex(x => x.NombreConsultora)
            .HasDatabaseName("ix_consultora_nombreconsultora");

        builder.HasIndex(x => x.RucConsultora)
            .HasDatabaseName("ix_consultora_rucconsultora");

        builder.HasIndex(x => x.CorreoOrganizacional)
            .HasDatabaseName("ix_consultora_correoorganizacional");

    }
}
