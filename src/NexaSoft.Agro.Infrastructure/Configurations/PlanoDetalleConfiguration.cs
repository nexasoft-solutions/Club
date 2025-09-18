using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class PlanoDetalleConfiguration : IEntityTypeConfiguration<PlanoDetalle>
{
    public void Configure(EntityTypeBuilder<PlanoDetalle> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Coordenadas)
              .HasColumnType("geometry")
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
    }
}
