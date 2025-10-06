using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberPinConfiguration: IEntityTypeConfiguration<MemberPin>
{
    public void Configure(EntityTypeBuilder<MemberPin> builder)
    {
        builder.ToTable("member_pins");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MemberId)
            .HasColumnName("member_id")
            .IsRequired();

        builder.Property(x => x.Pin)
            .HasColumnName("pin")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(x => x.DeviceId)
            .HasColumnName("device_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsUsed)
            .HasColumnName("is_used")
            .IsRequired();

        builder.Property(x => x.UsedAt)
            .HasColumnName("used_at")
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasColumnName("deleted_by")
            .HasMaxLength(40)
            .IsRequired(false);

        // Relación con Member
        builder.HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .HasConstraintName("fk_member_pins_members")
            .OnDelete(DeleteBehavior.Cascade);

        // Índices recomendados
        builder.HasIndex(x => x.MemberId)
            .HasDatabaseName("ix_memberpins_memberid");

        builder.HasIndex(x => x.Pin)
            .HasDatabaseName("ix_memberpins_pin");

        builder.HasIndex(x => x.ExpiresAt)
            .HasDatabaseName("ix_memberpins_expiresat");

        builder.HasIndex(x => x.IsUsed)
            .HasDatabaseName("ix_memberpins_isused");
    }
}
