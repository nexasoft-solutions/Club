using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberRefreshTokenConfiguration: IEntityTypeConfiguration<MemberRefreshToken>
{
    public void Configure(EntityTypeBuilder<MemberRefreshToken> builder)
    {
        builder.ToTable("member_refresh_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Token)
            .HasColumnName("token")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.MemberId)
            .HasColumnName("member_id")
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(x => x.IsRevoked)
            .HasColumnName("is_revoked")
            .IsRequired();

        builder.Property(x => x.RevokedAt)
            .HasColumnName("revoked_at")
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasColumnName("deleted_by")
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);

        // Relaciones
        builder.HasOne(x => x.Member)
            .WithMany() //Si en Member tienes una colección, cámbialo por `.WithMany(x => x.RefreshTokens)`
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices
        builder.HasIndex(x => x.Token)
            .HasDatabaseName("ix_refresh_token_token")
            .IsUnique();

        builder.HasIndex(x => x.MemberId)
            .HasDatabaseName("ix_refresh_token_memberid");

        builder.HasIndex(x => x.ExpiresAt)
            .HasDatabaseName("ix_refresh_token_expiresat");

        builder.HasIndex(x => x.IsRevoked)
            .HasDatabaseName("ix_refresh_token_isrevoked");
    }
}