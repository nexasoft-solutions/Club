using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Dni)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Phone)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.DepartamentId)
            .IsRequired();

        builder.HasOne(x => x.Departament)
               .WithMany()
               .HasForeignKey(x => x.DepartamentId)
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.ProvinceId)
            .IsRequired();  

        builder.HasOne(x => x.Province)
               .WithMany()
               .HasForeignKey(x => x.ProvinceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DistrictId)
            .IsRequired();  

        builder.HasOne(x => x.District)
               .WithMany()
               .HasForeignKey(x => x.DistrictId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Address)
            .IsRequired(false);

        builder.Property(x => x.StatusId)
            .IsRequired(false);

        builder.Property(x => x.BirthDate)
            .IsRequired(false);

        builder.Property(x => x.MemberTypeId)
            .IsRequired();


        builder.HasOne(x => x.MemberType)
               .WithMany()
               .HasForeignKey(x => x.MemberTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.MemberStatusId)
            .IsRequired();

      
        builder.HasOne(x => x.MemberStatus)
               .WithMany()
               .HasForeignKey(x => x.MemberStatusId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EntryFeePaid)
            .IsRequired();

        builder.HasOne(x => x.Status)
               .WithMany()
               .HasForeignKey(x => x.StatusId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.JoinDate)
            .IsRequired();

        builder.Property(x => x.ExpirationDate)
            .IsRequired(false);

        builder.Property(x => x.Balance)
            .IsRequired();

     

        builder.Property(x => x.LastPaymentDate)
            .IsRequired(false);


        builder.Property(x => x.StateMember)
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

        builder.HasIndex(x => x.Dni)
            .HasDatabaseName("ix_member_dni");

        builder.HasIndex(x => x.Email)
            .HasDatabaseName("ix_member_email");

        builder.HasIndex(x => x.MemberTypeId)
            .HasDatabaseName("ix_member_membertypeid");

        builder.HasIndex(x => x.MemberStatusId)
            .HasDatabaseName("ix_member_statusid");

    

    }
}
