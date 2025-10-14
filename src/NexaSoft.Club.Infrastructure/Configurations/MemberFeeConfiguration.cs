using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberFeeConfiguration : IEntityTypeConfiguration<MemberFee>
{
    public void Configure(EntityTypeBuilder<MemberFee> builder)
    {
        builder.ToTable("member_fees");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MemberId)
            .IsRequired();


        builder.HasOne(x => x.Member)
               .WithMany()
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.MemberTypeFeeId)
            .IsRequired(false);


        builder.HasOne(x => x.MemberTypeFee)
               .WithMany()
               .HasForeignKey(x => x.MemberTypeFeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Period)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.Property(x => x.StatusId)
            .IsRequired();

        builder.HasOne(x => x.Status)
           .WithMany()
           .HasForeignKey(x => x.StatusId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StateMemberFee)
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
            .HasDatabaseName("ix_memberfee_memberid");

        builder.HasIndex(x => x.MemberTypeFeeId)
            .HasDatabaseName("ix_memberfee_configid");

        builder.HasIndex(x => x.Period)
            .HasDatabaseName("ix_memberfee_period");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_memberfee_status");

        builder.HasIndex(x => new { x.MemberId, x.StatusId })
            .HasDatabaseName("idx_member_fees_member_status");

    }
}
