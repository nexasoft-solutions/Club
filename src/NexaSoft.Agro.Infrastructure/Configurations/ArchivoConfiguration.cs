using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class ArchivoConfiguration : IEntityTypeConfiguration<Archivo>
{
    public void Configure(EntityTypeBuilder<Archivo> builder)
    {
        /*builder.ToTable("archivos", table =>
        {
            table.HasCheckConstraint(
                "CK_Archivo_SubCapituloOrEstructura",
                @"(""sub_capitulo_id"" IS NOT NULL AND ""estructura_id"" IS NULL) 
                OR (""sub_capitulo_id"" IS NULL AND ""estructura_id"" IS NOT NULL)"
            );
        });*/

        builder.ToTable("archivos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombreArchivo)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.DescripcionArchivo)
            .HasMaxLength(550)
            .IsRequired(false);

        builder.Property(x => x.RutaArchivo)
            .HasMaxLength(350)
            .IsRequired(false);

        builder.Property(x => x.FechaCarga)
            .IsRequired();

        builder.Property(x => x.TipoArchivoId)
            .IsRequired();    

        builder.Property(x => x.EstadoArchivo)
            .IsRequired();
            
        
        builder.HasIndex(x => x.DescripcionArchivo)
            .HasDatabaseName("ix_archivo_descripcion");
            

    }
}
