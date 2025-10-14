using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class UserQrHistoryConfiguration : IEntityTypeConfiguration<UserQrHistory>
{
    public void Configure(EntityTypeBuilder<UserQrHistory> builder)
    {
        builder.ToTable("user_qr_historys");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(uh => uh.QrCode)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(uh => uh.QrUrl)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(uh => uh.ExpirationDate)
            .IsRequired(false);

        builder.Property(x => x.CreatedBy)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
         .HasMaxLength(40)
         .IsRequired(false);

        builder.Property(x => x.UserId)
            .IsRequired();

       builder.HasOne(x => x.User)
               .WithMany(x => x.QrHistory)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_userqrhistory_userid");

        builder.HasIndex(x => x.QrCode)
            .HasDatabaseName("ix_userqrhistory_qrcode");

        builder.HasIndex(x => x.ExpirationDate)
            .HasDatabaseName("ix_userqrhistory_expirationdate");
    }
}
