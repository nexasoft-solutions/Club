using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.Positions;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(60)
            .IsRequired(false);

        builder.Property(x => x.DepartmentId)
            .IsRequired(false);


         builder.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EmployeeTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.EmployeeType)
                .WithMany()
                .HasForeignKey(x => x.EmployeeTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.BaseSalary)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.StatePosition)
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
            .HasDatabaseName("ix_position_code");

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("ix_position_name");

        builder.HasIndex(x => x.DepartmentId)
            .HasDatabaseName("ix_position_departmentid");

        builder.HasIndex(x => x.EmployeeTypeId)
            .HasDatabaseName("ix_position_employeetypeid");

    }
}
