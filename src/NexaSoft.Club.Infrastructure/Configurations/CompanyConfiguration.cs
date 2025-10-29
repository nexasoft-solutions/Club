using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Ruc)
            .HasMaxLength(11)
            .IsRequired(false);

        builder.Property(x => x.BusinessName)
            .HasMaxLength(220)
            .IsRequired(false);

        builder.Property(x => x.TradeName)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.Address)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(x => x.Phone)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.Website)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.StateCompany)
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

        builder.HasIndex(x => x.Ruc)
            .HasDatabaseName("ix_company_ruc");

        builder.HasIndex(x => x.BusinessName)
            .HasDatabaseName("ix_company_businessname");

    }
}
