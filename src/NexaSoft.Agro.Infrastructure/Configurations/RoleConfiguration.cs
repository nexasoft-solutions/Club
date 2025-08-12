using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Agro.Domain.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        /*builder.Ignore(r => r.RolePermissions);

        builder.HasMany<RolePermission>("_rolePermissions")
            .WithOne()
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation("_rolePermissions")
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);*/


        /*builder.Ignore(r => r.UserRoles);

        // Configuración de la relación con usuarios
        builder.HasMany<UserRole>("_userRoles")
            .WithOne()
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation("_userRoles")
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);*/

        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasDatabaseName("ix_role_name");

    }

}
