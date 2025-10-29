using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class AttendanceRecordConfiguration : IEntityTypeConfiguration<AttendanceRecord>
{
    public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
    {
        builder.ToTable("attendance_records");
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

        builder.Property(x => x.CostCenterId)
            .IsRequired(false);


        builder.HasOne(x => x.CostCenter)
               .WithMany()
               .HasForeignKey(x => x.CostCenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RecordDate)
            .IsRequired();

        builder.Property(x => x.CheckInTime)
            .IsRequired(false);

        builder.Property(x => x.CheckOutTime)
            .IsRequired(false);

        builder.Property(x => x.TotalHours)
            .IsRequired(false);

        builder.Property(x => x.RegularHours)
            .IsRequired(false);

        builder.Property(x => x.OvertimeHours)
            .IsRequired(false);

        builder.Property(x => x.SundayHours)
            .IsRequired(false);

        builder.Property(x => x.HolidayHours)
            .IsRequired(false);

        builder.Property(x => x.NightHours)
            .IsRequired(false);

        builder.Property(x => x.LateMinutes)
            .IsRequired(false);

        builder.Property(x => x.EarlyDepartureMinutes)
            .IsRequired(false);

        builder.Property(x => x.AttendanceStatusTypeId)
            .IsRequired(false);


        builder.HasOne(x => x.AttendanceStatusType)
               .WithMany()
               .HasForeignKey(x => x.AttendanceStatusTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StateAttendanceRecord)
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
            .HasDatabaseName("ix_attendancerecord_employeeid");

        builder.HasIndex(x => x.CostCenterId)
            .HasDatabaseName("ix_attendancerecord_costcenterid");

        builder.HasIndex(x => x.AttendanceStatusTypeId)
            .HasDatabaseName("ix_attendancerecord_attendancestatustypeid");
        builder.HasIndex(x => new { x.EmployeeId, x.RecordDate })
            .IsUnique()
            .HasDatabaseName("unique_attendance");

    }
}
