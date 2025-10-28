using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
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

        builder.Property(x => x.ParentDepartmentId)
            .IsRequired(false);


         builder.HasOne(x => x.ParentDepartment)
                .WithMany()
                .HasForeignKey(x => x.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.ManagerId)
            .IsRequired(false);


         builder.HasOne(x => x.EmployeeInfo)
                .WithMany()
                .HasForeignKey(x => x.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CostCenterId)
            .IsRequired(false);


         builder.HasOne(x => x.CostCenter)
                .WithMany()
                .HasForeignKey(x => x.CostCenterId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Location)
            .HasMaxLength(120)
            .IsRequired(false);

        builder.Property(x => x.PhoneExtension)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.StateDepartment)
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
            .HasDatabaseName("ix_department_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_department_name");

        builder.HasIndex(x => x.ParentDepartmentId)
            .HasDatabaseName("ix_department_parentdepartmentid");

        builder.HasIndex(x => x.ManagerId)
            .HasDatabaseName("ix_department_managerid");

        builder.HasIndex(x => x.CostCenterId)
            .HasDatabaseName("ix_department_costcenterid");

    }
}
