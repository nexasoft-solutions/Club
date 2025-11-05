using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class EmployeeInfoConfiguration : IEntityTypeConfiguration<EmployeeInfo>
{
    public void Configure(EntityTypeBuilder<EmployeeInfo> builder)
    {
        builder.ToTable("employees_info");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EmployeeCode)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.UserId)
            .IsRequired(false);


        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PositionId)
            .IsRequired(false);


        builder.HasOne(x => x.Position)
               .WithMany()
               .HasForeignKey(x => x.PositionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.EmployeeTypeId)
            .IsRequired(false);


        builder.HasOne(x => x.EmployeeType)
               .WithMany()
               .HasForeignKey(x => x.EmployeeTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DepartmentId)
            .IsRequired(false);


        builder.HasOne(x => x.Department)
               .WithMany()
               .HasForeignKey(x => x.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.HireDate)
            .IsRequired();

        builder.Property(x => x.BaseSalary)
            .IsRequired();

        builder.Property(x => x.PaymentMethodId)
            .IsRequired(false);


        builder.HasOne(x => x.PaymentMethodType)
               .WithMany()
               .HasForeignKey(x => x.PaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.BankId)
            .IsRequired(false);


        builder.HasOne(x => x.Bank)
               .WithMany()
               .HasForeignKey(x => x.BankId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.BankAccountTypeId)
            .IsRequired(false);


        builder.HasOne(x => x.BankAccountType)
               .WithMany()
               .HasForeignKey(x => x.BankAccountTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CurrencyId)
            .IsRequired(false);


        builder.HasOne(x => x.Currency)
               .WithMany()
               .HasForeignKey(x => x.CurrencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Company)
               .WithMany()
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CostCenterId)
            .IsRequired(false);
            
        builder.HasOne(x => x.CostCenter)
               .WithMany()
               .HasForeignKey(x => x.CostCenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.BankAccountNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.CciNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.StateEmployeeInfo)
            .IsRequired();

        builder.Property(x => x.IsFamilyAllowance)
            .IsRequired(false);

       

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(40)
            .IsRequired(false);

        builder.HasIndex(x => x.EmployeeCode)
            .HasDatabaseName("ix_employeeinfo_employeecode");

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_employeeinfo_userid");

        builder.HasIndex(x => x.PositionId)
            .HasDatabaseName("ix_employeeinfo_positionid");

        builder.HasIndex(x => x.EmployeeTypeId)
            .HasDatabaseName("ix_employeeinfo_employeetypeid");

        builder.HasIndex(x => x.DepartmentId)
            .HasDatabaseName("ix_employeeinfo_departmentid");

        builder.HasIndex(x => x.PaymentMethodId)
            .HasDatabaseName("ix_employeeinfo_paymentmethodid");

        builder.HasIndex(x => x.BankId)
            .HasDatabaseName("ix_employeeinfo_bankid");

        builder.HasIndex(x => x.BankAccountTypeId)
            .HasDatabaseName("ix_employeeinfo_bankaccounttypeid");

        builder.HasIndex(x => x.CurrencyId)
            .HasDatabaseName("ix_employeeinfo_currencyid");

    }
}
