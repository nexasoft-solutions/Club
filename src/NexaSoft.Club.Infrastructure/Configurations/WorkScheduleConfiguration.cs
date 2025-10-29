using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexaSoft.Club.Domain.HumanResources.WorkSchedules;

namespace NexaSoft.Club.Infrastructure.Configurations;

public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.ToTable("work_schedules");
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

        builder.Property(x => x.DayOfWeek)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired(false);

        builder.Property(x => x.EndTime)
            .IsRequired(false);

        builder.Property(x => x.StateWorkSchedule)
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
            .HasDatabaseName("ix_workschedule_employeeid");

    }
}
