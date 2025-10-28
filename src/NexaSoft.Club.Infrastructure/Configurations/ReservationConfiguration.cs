using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MemberId)
            .IsRequired();

        builder.HasOne(x => x.SpaceAvailability)
                .WithMany()
                .HasForeignKey(x => x.SpaceAvailabilityId)
                .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Member)
               .WithMany()
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.SpaceId)
            .IsRequired();

        builder.Property(x => x.SpaceAvailabilityId)
          .IsRequired();


        builder.HasOne(x => x.Space)
               .WithMany()
               .HasForeignKey(x => x.SpaceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.StatusId)
            .IsRequired(false);

        builder.Property(x => x.TotalAmount)
            .IsRequired();

        builder.Property(x => x.AccountingEntryId)
            .IsRequired(false);

        builder.Property(x => x.WeekNumber)
            .IsRequired();

        builder.Property(x => x.Year)
          .IsRequired();

        builder.HasOne(x => x.Status)
               .WithMany()
               .HasForeignKey(x => x.StatusId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AccountingEntry)
                .WithMany()
                .HasForeignKey(x => x.AccountingEntryId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.PaymentMethodId)
            .IsRequired();

        builder.HasOne(x => x.PaymentType)
                .WithMany()
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DocumentTypeId)
            .IsRequired();

        builder.HasOne(x => x.DocumentType)
                .WithMany()
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ReferenceNumber)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.ReceiptNumber)
            .HasMaxLength(100)
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

        builder.HasIndex(x => x.MemberId)
            .HasDatabaseName("ix_reservation_memberid");

        builder.HasIndex(x => x.SpaceId)
            .HasDatabaseName("ix_reservation_spaceid");

        builder.HasIndex(x => x.StartTime)
            .HasDatabaseName("ix_reservation_starttime");

        builder.HasIndex(x => new { x.Year, x.WeekNumber, x.SpaceId })
            .HasDatabaseName("ix_reservation_year_weeknumber_spaceid");

        builder.HasIndex(x => x.Date)
            .HasDatabaseName("ix_reservation_date");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_reservation_status");

        builder.HasIndex(x => x.AccountingEntryId)
            .HasDatabaseName("ix_reservation_accountingentryid");

    }
}
