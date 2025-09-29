using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberTypeConfiguration : IEntityTypeConfiguration<MemberType>
{
    public void Configure(EntityTypeBuilder<MemberType> builder)
    {
        builder.ToTable("member_types");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TypeName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.HasFamilyDiscount)
            .IsRequired(false);

        builder.Property(x => x.MaxFamilyMembers)
            .IsRequired(false);

        builder.Property(x => x.FamilyDiscountRate)
            .IsRequired(false);

        builder.Property(x => x.StateMemberType)
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

        builder.HasIndex(x => x.TypeName)
            .HasDatabaseName("ix_membertype_typename");


    }
}
