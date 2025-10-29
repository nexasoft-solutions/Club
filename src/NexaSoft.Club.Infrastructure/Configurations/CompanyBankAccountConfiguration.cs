using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class CompanyBankAccountConfiguration : IEntityTypeConfiguration<CompanyBankAccount>
{
    public void Configure(EntityTypeBuilder<CompanyBankAccount> builder)
    {
        builder.ToTable("company_bank_accounts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
            .IsRequired(false);


         builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
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

        builder.Property(x => x.BankAccountNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.CciNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.CurrencyId)
            .IsRequired(false);


         builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsPrimary)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.StateCompanyBankAccount)
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

        builder.HasIndex(x => x.CompanyId)
            .HasDatabaseName("ix_companybankaccount_companyid");

        builder.HasIndex(x => x.BankId)
            .HasDatabaseName("ix_companybankaccount_bankid");

        builder.HasIndex(x => x.BankAccountTypeId)
            .HasDatabaseName("ix_companybankaccount_bankaccounttypeid");

        builder.HasIndex(x => x.CurrencyId)
            .HasDatabaseName("ix_companybankaccount_currencyid");

    }
}
