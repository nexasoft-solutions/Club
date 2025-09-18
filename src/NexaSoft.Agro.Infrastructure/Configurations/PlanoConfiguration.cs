using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class PlanoConfiguration : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.ToTable("planos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EscalaId)
            .IsRequired();

        builder.Property(x => x.SistemaProyeccion)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(x => x.NombrePlano)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.ArchivoId)
            .IsRequired();


        builder.Property(x => x.ColaboradorId)
            .IsRequired();

        builder.Property(x => x.EstadoPlano)
            .IsRequired();


        builder.HasOne(x => x.Colaborador)
               .WithMany()
               .HasForeignKey(x => x.ColaboradorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Detalles)
                .WithOne(d => d.Plano)
                .HasForeignKey(d => d.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

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
