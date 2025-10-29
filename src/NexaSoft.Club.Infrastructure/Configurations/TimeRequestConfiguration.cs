using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class TimeRequestConfiguration : IEntityTypeConfiguration<TimeRequest>
{
    public void Configure(EntityTypeBuilder<TimeRequest> builder)
    {
        builder.ToTable("time_requests");
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

        builder.Property(x => x.TimeRequestTypeId)
            .IsRequired(false);


         builder.HasOne(x => x.TimeRequestType)
                .WithMany()
                .HasForeignKey(x => x.TimeRequestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StartDate)
            .IsRequired(false);

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.TotalDays)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.StatusId)
            .IsRequired(false);


         builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StateTimeRequest)
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

        builder.HasIndex(x => x.EmployeeId)
            .HasDatabaseName("ix_timerequest_employeeid");

        builder.HasIndex(x => x.TimeRequestTypeId)
            .HasDatabaseName("ix_timerequest_timerequesttypeid");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_timerequest_statusid");

    }
}
