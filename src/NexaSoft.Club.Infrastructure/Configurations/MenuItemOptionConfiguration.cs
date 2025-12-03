using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MenuItemOptionConfiguration : IEntityTypeConfiguration<MenuItemOption>
{
    public void Configure(EntityTypeBuilder<MenuItemOption> builder)
    {
        builder.ToTable("menu_item_options");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Label)
            .HasColumnName("label")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Icon)
            .HasColumnName("icon")
            .HasMaxLength(300);

        builder.Property(x => x.Route)
            .HasColumnName("route")
            .HasMaxLength(200);

        builder.Property(x => x.ParentId)
            .HasColumnName("parent_id")
            .IsRequired(false);
        
        builder.Property(x => x.StateMenu)
            .HasColumnName("state_menu")
            .IsRequired();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.MenuItemOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);            

        builder.Property(x => x.DeletedBy)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.HasIndex(x => x.Label)
            .HasDatabaseName("ix_menu_label");
    }
}
