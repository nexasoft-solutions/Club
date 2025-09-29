using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MenuRoleConfiguration : IEntityTypeConfiguration<MenuRole>
{
    public void Configure(EntityTypeBuilder<MenuRole> builder)
    {
        builder.ToTable("menu_roles");

        builder.HasKey(x => new { x.MenuItemOptionId, x.RoleId });

        builder.Property(x => x.MenuItemOptionId)
            .HasColumnName("menu_item_option_id");

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id");

        builder.HasIndex(x => x.RoleId)
            .HasDatabaseName("ix_menu_roles_role_id");
    }
}
