using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class CapituloConfiguration : IEntityTypeConfiguration<Capitulo>
{
    public void Configure(EntityTypeBuilder<Capitulo> builder)
    {
        builder.ToTable("capitulos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreCapitulo)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.DescripcionCapitulo)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.EstadoCapitulo)
            .IsRequired();

        builder.Property(x => x.EstudioAmbientalId)
            .IsRequired();


        builder.HasOne(x => x.EstudioAmbiental)
               .WithMany()
               .HasForeignKey(x => x.EstudioAmbientalId)
               .OnDelete(DeleteBehavior.Restrict);

        
        builder.HasIndex(x => x.NombreCapitulo)
            .HasDatabaseName("ix_capitulo_nombre");
            

        /*builder.HasMany(c => c.SubCapitulos)
               .WithOne(sc => sc.Capitulo)
               .HasForeignKey(sc => sc.CapituloId);*/

    }
}
