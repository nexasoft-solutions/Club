using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class PayrollPaymentRecordConfiguration: IEntityTypeConfiguration<PayrollPaymentRecord>
{
    public void Configure(EntityTypeBuilder<PayrollPaymentRecord> builder)
    {
        builder.ToTable("payroll_payment_records");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PayrollDetailId)
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();

        builder.Property(x => x.PaymentMethodId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(12,2)")
            .IsRequired();

        builder.Property(x => x.CurrencyId)
            .IsRequired();

        builder.Property(x => x.ExchangeRate)
            .HasColumnType("decimal(8,4)")
            .HasDefaultValue(1.0m);

        builder.Property(x => x.Reference)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.BankId)
            .IsRequired(false);

        builder.Property(x => x.CompanyBankAccountId)
            .IsRequired(false);

        builder.Property(x => x.StatusId)
            .IsRequired(false);

        builder.Property(x => x.PaymentFilePath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.ConfirmationDocumentPath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.ProcessedAt)
            .IsRequired(false);

        builder.Property(x => x.ProcessedById)
            .IsRequired(false);

        // Relaciones
        builder.HasOne(x => x.PayrollDetail)
            .WithMany()
            .HasForeignKey(x => x.PayrollDetailId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PaymentMethod)
            .WithMany()
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Bank)
            .WithMany()
            .HasForeignKey(x => x.BankId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CompanyBankAccount)
            .WithMany()
            .HasForeignKey(x => x.CompanyBankAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

       

        builder.Property(x => x.StatePayrollPaymentRecord)
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

        // Índices
        builder.HasIndex(x => x.PayrollDetailId)
            .HasDatabaseName("ix_payrollpaymentrecord_payrolldetailid");

        builder.HasIndex(x => x.PaymentMethodId)
            .HasDatabaseName("ix_payrollpaymentrecord_paymentmethodid");

        builder.HasIndex(x => x.CurrencyId)
            .HasDatabaseName("ix_payrollpaymentrecord_currencyid");

        builder.HasIndex(x => x.BankId)
            .HasDatabaseName("ix_payrollpaymentrecord_bankid");

        builder.HasIndex(x => x.CompanyBankAccountId)
            .HasDatabaseName("ix_payrollpaymentrecord_companybankaccountid");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_payrollpaymentrecord_statusid");

        builder.HasIndex(x => x.ProcessedById)
            .HasDatabaseName("ix_payrollpaymentrecord_processedbyid");
    }
}