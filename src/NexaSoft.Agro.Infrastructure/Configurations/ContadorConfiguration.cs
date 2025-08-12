using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Contadores;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class ContadorConfiguration : IEntityTypeConfiguration<Contador>
{
    public void Configure(EntityTypeBuilder<Contador> builder)
    {
        builder.ToTable("contadores");

        // Clave primaria
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Entidad)
            .HasColumnName("entidad")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Prefijo)
            .HasColumnName("prefijo")
            .HasMaxLength(10)
            .IsRequired(false);

        builder.Property(x => x.ValorActual)
            .HasColumnName("valor_actual")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.Agrupador)
            .HasColumnName("agrupador")
            .HasMaxLength(50)
            .IsRequired(false);

        

        // Index para buscar por entidad y agrupador
        builder.HasIndex(x => new { x.Entidad, x.Agrupador })
            .HasDatabaseName("ix_contador_entidad_agrupador");
    
    }
}
