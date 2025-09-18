using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class ResponsableConfiguration : IEntityTypeConfiguration<Responsable>
{
    public void Configure(EntityTypeBuilder<Responsable> builder)
    {
        builder.ToTable("responsables");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreResponsable)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.CargoResponsable)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.CorreoResponsable)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.TelefonoResponsable)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Observaciones)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.EstadoResponsable)
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

        builder.HasOne(x => x.EstudioAmbiental)
             .WithMany()
             .HasForeignKey(x => x.EstudioAmbientalId)
             .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.NombreResponsable)
            .HasDatabaseName("ix_responsable_nombreresponsable");

        builder.HasIndex(x => x.CorreoResponsable)
            .HasDatabaseName("ix_responsable_correoresponsable");

    }
}
