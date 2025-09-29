using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Auth;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
         .HasColumnName("id") // Nombre de la columna en la base de datos
         .ValueGeneratedOnAdd();

        builder.Property(x => x.Revoked)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);            

        builder.Property(x => x.DeletedBy)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.HasIndex(x => new { x.Token })
            .HasDatabaseName("ix_token");

        builder.HasIndex(x => new { x.UserId })
            .HasDatabaseName("ix_user_id");

        builder.HasIndex(x => new { x.Expires })
            .HasDatabaseName("ix_expires");
    }
}
