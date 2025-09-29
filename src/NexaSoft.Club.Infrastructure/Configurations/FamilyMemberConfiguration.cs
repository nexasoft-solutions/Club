using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.FamilyMembers;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.ToTable("family_members");
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

        builder.Property(x => x.Dni)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Relationship)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.BirthDate)
            .IsRequired(false);

        builder.Property(x => x.IsAuthorized)
            .IsRequired();

        builder.Property(x => x.StateFamilyMember)
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
            .HasDatabaseName("ix_familymember_memberid");

        builder.HasIndex(x => x.Dni)
            .HasDatabaseName("ix_familymember_dni");

        builder.HasIndex(x => x.Relationship)
            .HasDatabaseName("ix_familymember_relationship");

    }
}
