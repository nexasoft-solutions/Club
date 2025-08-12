using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class ConstanteConfiguration : IEntityTypeConfiguration<Constante>
{
    public void Configure(EntityTypeBuilder<Constante> builder)
    {
        builder.ToTable("constantes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TipoConstante)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(x => x.Clave)
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.EstadoConstante)
            .IsRequired();

        builder.HasIndex(x => x.TipoConstante)
            .HasDatabaseName("ix_constante_tipoconstante");

        builder.HasIndex(x => new { x.Clave, x.Valor })
            .HasDatabaseName("ix_constante_clave_valor");

    }
}
