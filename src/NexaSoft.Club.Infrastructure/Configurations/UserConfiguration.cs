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

        builder.Property(x => x.LastName)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.FullName)
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

        builder.Property(x => x.Dni)
            .HasMaxLength(8)
            .IsRequired(false);

        builder.Property(x => x.Phone)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.MemberId)
            .IsRequired(false);

        builder.Property(x => x.UserTypeId)
            .IsRequired();

        builder.HasOne(x => x.UserType)
            .WithMany()
            .HasForeignKey(x => x.UserTypeId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.UserStatus)
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

        builder.HasIndex(x => x.FullName)
            .HasDatabaseName("ix_user_fullname");

        builder.HasIndex(x => x.UserName)
            .HasDatabaseName("ix_user_username");

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("ix_user_email");

        builder.HasIndex(x => x.Dni)
            .HasDatabaseName("ix_user_userdni");

    }
}
