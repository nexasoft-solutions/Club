using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSpaceAva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "max_reservation_days_in_advance",
                table: "space_availabilities");

            migrationBuilder.DropColumn(
                name: "max_reservations_per_day",
                table: "space_availabilities");

            migrationBuilder.DropColumn(
                name: "min_reservation_hours",
                table: "space_availabilities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "max_reservation_days_in_advance",
                table: "space_availabilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "max_reservations_per_day",
                table: "space_availabilities",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "min_reservation_hours",
                table: "space_availabilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
