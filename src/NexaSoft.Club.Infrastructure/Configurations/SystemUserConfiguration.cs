using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.SystemUsers;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
{
    public void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder.ToTable("system_users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Username)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Role)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.StateSystemUser)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.HasIndex(x => x.Username)
            .HasDatabaseName("ix_systemuser_username");

        builder.HasIndex(x => x.Email)
            .HasDatabaseName("ix_systemuser_email");

        builder.HasIndex(x => x.Role)
            .HasDatabaseName("ix_systemuser_role");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_systemuser_isactive");

    }
}
