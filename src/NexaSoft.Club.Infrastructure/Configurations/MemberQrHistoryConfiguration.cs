using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberQrHistoryConfiguration: IEntityTypeConfiguration<MemberQrHistory>
{
    public void Configure(EntityTypeBuilder<MemberQrHistory> builder)
    {
        builder.ToTable("member_qr_history");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MemberId)
            .IsRequired();

        // Relación con Member
        builder.HasOne(x => x.Member)
               .WithMany(x => x.QrHistory)
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.QrCode)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.QrUrl)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(x => x.ExpirationDate)
            .IsRequired(false);

      
        // Campos de auditoría (si heredas de BaseEntity)
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedAt)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        // 🔥 ÍNDICES para optimizar consultas
        builder.HasIndex(x => x.MemberId)
            .HasDatabaseName("ix_memberqrhistory_memberid");

        builder.HasIndex(x => x.QrCode)
            .HasDatabaseName("ix_memberqrhistory_qrcode");

       
        builder.HasIndex(x => x.ExpirationDate)
            .HasDatabaseName("ix_memberqrhistory_expiration");

        // Índice compuesto para búsquedas comunes
        /*builder.HasIndex(x => new { x.MemberId, x.GeneratedAt })
            .HasDatabaseName("ix_memberqrhistory_member_generated")
            .IsDescending(false, true); // Orden descendente por fecha de generación*/
    }
}
