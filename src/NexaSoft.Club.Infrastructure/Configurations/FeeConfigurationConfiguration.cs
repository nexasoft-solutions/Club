using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class FeeConfigurationConfiguration : IEntityTypeConfiguration<FeeConfiguration>
{
    public void Configure(EntityTypeBuilder<FeeConfiguration> builder)
    {
        builder.ToTable("fee_configurations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
   

        builder.HasOne(x => x.Periodicity)
               .WithMany()
               .HasForeignKey(x => x.PeriodicityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.FeeName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.DefaultAmount)
            .IsRequired(false);

       
        builder.Property(x => x.DueDay)
            .IsRequired(false);

        builder.Property(x => x.IsVariable)
            .IsRequired();

        builder.Property(x => x.AppliesToFamily)
            .IsRequired();

        builder.Property(x => x.IncomeAccountId)
            .IsRequired(false);


        builder.HasOne(x => x.IncomeAccount)
               .WithMany()
               .HasForeignKey(x => x.IncomeAccountId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StateFeeConfiguration)
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


        builder.HasIndex(x => x.FeeName)
            .HasDatabaseName("ix_feeconfiguration_feename");

        builder.HasIndex(x => x.PeriodicityId)
            .HasDatabaseName("ix_feeconfiguration_periodicity");

    }
}
