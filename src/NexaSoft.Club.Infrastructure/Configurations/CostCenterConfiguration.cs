using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter>
{
    public void Configure(EntityTypeBuilder<CostCenter> builder)
    {
        builder.ToTable("cost_centers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.ParentCostCenterId)
            .IsRequired(false);


         builder.HasOne(x => x.ParentCostCenter)
                .WithMany()
                .HasForeignKey(x => x.ParentCostCenterId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CostCenterTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.CostCenterType)
                .WithMany()
                .HasForeignKey(x => x.CostCenterTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.ResponsibleId)
            .IsRequired(false);


         builder.HasOne(x => x.EmployeeInfo)
                .WithMany()
                .HasForeignKey(x => x.ResponsibleId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Budget)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.StateCostCenter)
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

        builder.HasIndex(x => x.Code)
            .HasDatabaseName("ix_costcenter_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_costcenter_name");

        builder.HasIndex(x => x.ParentCostCenterId)
            .HasDatabaseName("ix_costcenter_parentcostcenterid");

        builder.HasIndex(x => x.CostCenterTypeId)
            .HasDatabaseName("ix_costcenter_costcentertypeid");

        builder.HasIndex(x => x.ResponsibleId)
            .HasDatabaseName("ix_costcenter_responsibleid");

    }
}
