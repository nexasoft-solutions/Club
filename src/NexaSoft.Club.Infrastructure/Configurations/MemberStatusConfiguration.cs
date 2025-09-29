using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberStatusConfiguration : IEntityTypeConfiguration<MemberStatus>
{
    public void Configure(EntityTypeBuilder<MemberStatus> builder)
    {
        builder.ToTable("member_statuses");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.StatusName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.CanAccess)
            .IsRequired();

        builder.Property(x => x.CanReserve)
            .IsRequired();

        builder.Property(x => x.CanParticipate)
            .IsRequired();

        builder.Property(x => x.StateMemberStatus)
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

        builder.HasIndex(x => x.StatusName)
            .HasDatabaseName("ix_memberstatus_statusname");

    }
}
