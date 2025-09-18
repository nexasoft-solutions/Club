using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class OrganizacionConfiguration : IEntityTypeConfiguration<Organizacion>
{
    public void Configure(EntityTypeBuilder<Organizacion> builder)
    {
        builder.ToTable("organizaciones");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreOrganizacion)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.ContactoOrganizacion)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.TelefonoContacto)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.SectorId)
            .IsRequired();

        builder.Property(x => x.RucOrganizacion)
          .HasMaxLength(11)
          .IsRequired(false);

        builder.Property(x => x.Observaciones)
         .HasMaxLength(550)
         .IsRequired(false);

        builder.Property(x => x.EstadoOrganizacion)
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

        builder.HasIndex(x => x.NombreOrganizacion)
            .HasDatabaseName("ix_organizacion_nombreorganizacion");
            
        builder.HasIndex(x => x.RucOrganizacion)
            .HasDatabaseName("ix_organizacion_rucorganizacion");

    }
}
