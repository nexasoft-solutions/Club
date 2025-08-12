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
    }
}
