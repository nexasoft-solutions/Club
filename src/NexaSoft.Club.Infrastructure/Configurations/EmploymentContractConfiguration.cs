using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class EmploymentContractConfiguration : IEntityTypeConfiguration<EmploymentContract>
{
    public void Configure(EntityTypeBuilder<EmploymentContract> builder)
    {
        builder.ToTable("employment_contracts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EmployeeId)
            .IsRequired(false);


         builder.HasOne(x => x.EmployeeInfo)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ContractTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.ContractType)
                .WithMany()
                .HasForeignKey(x => x.ContractTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.Salary)
            .IsRequired();

        builder.Property(x => x.WorkingHours)
            .IsRequired();

        builder.Property(x => x.DocumentPath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.StateEmploymentContract)
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

        builder.Property(x => x.IsActive)
            .IsRequired(false);

        builder.HasIndex(x => x.EmployeeId)
            .HasDatabaseName("ix_employmentcontract_employeeid");

        builder.HasIndex(x => x.ContractTypeId)
            .HasDatabaseName("ix_employmentcontract_contracttypeid");

    }
}
