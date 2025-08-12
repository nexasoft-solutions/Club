using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class ColaboradorConfiguration : IEntityTypeConfiguration<Colaborador>
{
    public void Configure(EntityTypeBuilder<Colaborador> builder)
    {
        builder.ToTable("colaboradores");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NombresColaborador)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.ApellidosColaborador)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.NombreCompletoColaborador)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.TipoDocumentoId)
            .IsRequired();

        builder.Property(x => x.NumeroDocumentoIdentidad)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.FechaNacimiento)
            .IsRequired(false);

        builder.Property(x => x.GeneroColaboradorId)
            .IsRequired();

        builder.Property(x => x.EstadoCivilColaboradorId)
            .IsRequired();

        builder.Property(x => x.Direccion)
            .HasMaxLength(180)
            .IsRequired(false);

        builder.Property(x => x.CorreoElectronico)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.TelefonoMovil)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.CargoId)
            .IsRequired();

        builder.Property(x => x.DepartamentoId)
            .IsRequired();

        builder.Property(x => x.FechaIngreso)
            .IsRequired(false);

        builder.Property(x => x.Salario)
            .IsRequired(false);

        builder.Property(x => x.FechaCese)
            .IsRequired(false);

        builder.Property(x => x.Comentarios)
            .IsRequired(false);

        builder.Property(x => x.ConsultoraId)
            .IsRequired();

        builder.Property(x => x.EstadoColaborador)
            .IsRequired();


         builder.HasOne(x => x.Consultora)
                .WithMany()
                .HasForeignKey(x => x.ConsultoraId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.NombresColaborador)
            .HasDatabaseName("ix_colaborador_nombrescolaborador");

        builder.HasIndex(x => x.NombreCompletoColaborador)
            .HasDatabaseName("ix_colaborador_nombrecompletocolaborador");

        builder.HasIndex(x => x.NumeroDocumentoIdentidad)
            .HasDatabaseName("ix_colaborador_numerodocumentoidentidad");

        builder.HasIndex(x => x.GeneroColaboradorId)
            .HasDatabaseName("ix_colaborador_generocolaboradorid");

    }
}
