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

        /*builder.HasOne(x => x.Capitulo)
               .WithMany()
               .HasForeignKey(x => x.CapituloId)
               .OnDelete(DeleteBehavior.Restrict);*/

        /*builder.HasOne(s => s.Capitulo)
            .WithMany(c => c.SubCapitulos)
            .HasForeignKey(s => s.CapituloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(sc => sc.Estructuras)
                .WithOne(e => e.SubCapitulo)
                .HasForeignKey(e => e.SubCapituloId);

        builder.HasMany(sc => sc.Archivos)
               .WithOne(a => a.SubCapitulo)
               .HasForeignKey(a => a.SubCapituloId)
               .OnDelete(DeleteBehavior.Restrict);*/

    }
}
