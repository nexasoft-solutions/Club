using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.MemberVisits;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberVisitConfiguration : IEntityTypeConfiguration<MemberVisit>
{
    public void Configure(EntityTypeBuilder<MemberVisit> builder)
    {
        builder.ToTable("member_visits");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MemberId)
            .IsRequired(false);


         builder.HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.VisitDate)
            .IsRequired(false);

        builder.Property(x => x.EntryTime)
            .IsRequired(false);

        builder.Property(x => x.ExitTime)
            .IsRequired(false);

        builder.Property(x => x.QrCodeUsed)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.CheckInBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.CheckOutBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.VisitType)
            .IsRequired();

        builder.Property(x => x.StateMemberVisit)
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

        builder.HasIndex(x => x.MemberId)
            .HasDatabaseName("ix_membervisit_memberid");

        builder.HasIndex(x => x.VisitDate)
            .HasDatabaseName("ix_membervisit_visitdate");

        builder.HasIndex(x => x.EntryTime)
            .HasDatabaseName("ix_membervisit_entrytime");

        builder.HasIndex(x => x.ExitTime)
            .HasDatabaseName("ix_membervisit_exittime");

        builder.HasIndex(x => x.CheckInBy)
            .HasDatabaseName("ix_membervisit_checkinby");

    }
}
