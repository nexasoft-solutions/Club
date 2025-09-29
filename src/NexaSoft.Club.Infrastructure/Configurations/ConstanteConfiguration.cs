using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Constantes;

namespace NexaSoft.Club.Infrastructure.Configurations;

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

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.HasIndex(x => x.TipoConstante)
            .HasDatabaseName("ix_constante_tipoconstante");

        builder.HasIndex(x => new { x.Clave, x.Valor })
            .HasDatabaseName("ix_constante_clave_valor");

    }
}
