using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("empresas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RazonSocial)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.RucEmpresa)
            .HasMaxLength(11)
            .IsRequired(false);

        builder.Property(x => x.ContactoEmpresa)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.TelefonoContactoEmpresa)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.DepartamentoEmpresaId)
            .IsRequired();


         builder.HasOne(x => x.DepartamentoEmpresa)
                .WithMany()
                .HasForeignKey(x => x.DepartamentoEmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ProvinciaEmpresaId)
            .IsRequired();


         builder.HasOne(x => x.ProvinciaEmpresa)
                .WithMany()
                .HasForeignKey(x => x.ProvinciaEmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DistritoEmpresaId)
            .IsRequired();


         builder.HasOne(x => x.DistritoEmpresa)
                .WithMany()
                .HasForeignKey(x => x.DistritoEmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Direccion)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.LatitudEmpresa)
            .IsRequired();

        builder.Property(x => x.LongitudEmpresa)
            .IsRequired();

        builder.Property(x => x.OrganizacionId)
            .IsRequired();

        builder.Property(x => x.EstadoEmpresa)
            .IsRequired();


         builder.HasOne(x => x.Organizacion)
                .WithMany()
                .HasForeignKey(x => x.OrganizacionId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.RazonSocial)
            .HasDatabaseName("ix_empresa_razonsocial");

        builder.HasIndex(x => x.RucEmpresa)
            .HasDatabaseName("ix_empresa_rucempresa");

        builder.HasIndex(x => x.DepartamentoEmpresaId)
            .HasDatabaseName("ix_empresa_departamentoempresaid");

        builder.HasIndex(x => x.ProvinciaEmpresaId)
            .HasDatabaseName("ix_empresa_provinciaempresaid");

        builder.HasIndex(x => x.DistritoEmpresaId)
            .HasDatabaseName("ix_empresa_distritoempresaid");

        builder.HasIndex(x => x.LongitudEmpresa)
            .HasDatabaseName("ix_empresa_longitudempresa");

        builder.HasIndex(x => x.OrganizacionId)
            .HasDatabaseName("ix_empresa_organizacionid");

    }
}
