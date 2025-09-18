using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Permissions;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.ReferenciaControl)
            .HasMaxLength(50)
            .IsRequired();

        builder.Ignore(p => p.RolePermissions);

        builder.Property(x => x.UsuarioCreacion)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UsuarioModificacion)
            .HasMaxLength(40)
            .IsRequired(false);       

        builder.Property(x => x.UsuarioEliminacion)
         .HasMaxLength(40)
         .IsRequired(false); 

        builder.HasMany<RolePermission>("_rolePermissions")
            .WithOne()
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation("_rolePermissions")
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);


        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasDatabaseName("ix_permission_name");

        builder.HasIndex(x => x.ReferenciaControl)
            .HasDatabaseName("ix_permission_referenciacontrol");
    }
}
