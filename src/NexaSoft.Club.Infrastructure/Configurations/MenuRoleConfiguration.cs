using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MenuRoleConfiguration : IEntityTypeConfiguration<MenuRole>
{
    public void Configure(EntityTypeBuilder<MenuRole> builder)
    {
        builder.ToTable("menu_roles");

        // 🔑 Clave primaria compuesta
        builder.HasKey(x => new { x.MenuItemOptionId, x.RoleId });

        builder.Property(x => x.MenuItemOptionId)
            .HasColumnName("menu_item_option_id");

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id");

        // ✔ Relación con MenuItemOption
        builder.HasOne(x => x.MenuItemOption)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.MenuItemOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        // ✔ Relación con Role
        builder.HasOne(x => x.Role)
            .WithMany(x => x.MenuRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // ✔ Índice (aunque PK ya es índice)
        builder.HasIndex(x => new { x.MenuItemOptionId, x.RoleId })
            .HasDatabaseName("ix_menu_roles_menu_role");
    }
}
