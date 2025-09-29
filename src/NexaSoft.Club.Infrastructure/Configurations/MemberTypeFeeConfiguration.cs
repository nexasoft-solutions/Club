using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberTypeFeeConfiguration : IEntityTypeConfiguration<MemberTypeFee>
{
    public void Configure(EntityTypeBuilder<MemberTypeFee> builder)
    {
        builder.ToTable("member_types_fees");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FeeConfigurationId)
            .IsRequired();

        builder.Property(x => x.MemberTypeId)
            .IsRequired();

        builder.HasOne(x => x.FeeConfiguration)
               .WithMany()
               .HasForeignKey(x => x.FeeConfigurationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.MemberType)
               .WithMany()
               .HasForeignKey(x => x.MemberTypeId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.HasIndex(x => x.MemberTypeId)
            .HasDatabaseName("ix_membertype_id");

        builder.HasIndex(x => new {x.MemberTypeId, x.FeeConfigurationId})
            .HasDatabaseName("ix_membertype_fee_configuration");

    }
}
