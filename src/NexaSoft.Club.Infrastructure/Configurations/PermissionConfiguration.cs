using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Infrastructure.Configurations;

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

        builder.Property(x => x.Reference)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Source)
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(x => x.Action)
            .HasMaxLength(30)
            .IsRequired();

        builder.Ignore(p => p.RolePermissions);

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);       

        builder.Property(x => x.DeletedBy)
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

        builder.HasIndex(x => x.Reference)
            .HasDatabaseName("ix_permission_reference");

        builder.HasIndex(x => new { x.Source, x.Action })
            .IsUnique()
            .HasDatabaseName("ix_permission_source_action");
        
        builder.HasIndex(x => x.Source)
            .HasDatabaseName("ix_permission_source");
    }
}
