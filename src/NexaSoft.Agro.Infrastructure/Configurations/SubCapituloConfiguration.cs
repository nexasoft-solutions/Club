using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class SubCapituloConfiguration : IEntityTypeConfiguration<SubCapitulo>
{
    public void Configure(EntityTypeBuilder<SubCapitulo> builder)
    {
        builder.ToTable("subcapitulos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreSubCapitulo)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.DescripcionSubCapitulo)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.CapituloId)
            .IsRequired();

        builder.Property(x => x.EstadoSubCapitulo)
            .IsRequired();

        builder.HasIndex(x => x.NombreSubCapitulo)
          .HasDatabaseName("ix_subcapitulo_nombre");

        builder.Property(x => x.UsuarioCreacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioModificacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioEliminacion)
         .HasMaxLength(40)
         .IsRequired(false);

    }
}
