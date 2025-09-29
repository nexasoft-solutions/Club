using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserApellidos)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.UserNombres)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.NombreCompleto)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.UserName)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(x => x.Password)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.UserDni)
            .HasMaxLength(8)
            .IsRequired(false);

        builder.Property(x => x.UserTelefono)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.EstadoUser)
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

        builder.HasIndex(x => x.NombreCompleto)
            .HasDatabaseName("ix_user_nombrecompleto");

        builder.HasIndex(x => x.UserName)
            .HasDatabaseName("ix_user_username");

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("ix_user_email");

        builder.HasIndex(x => x.UserDni)
            .HasDatabaseName("ix_user_userdni");

    }
}
