using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReservationAddRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ix_payment_paymentdate",
                table: "payments",
                newName: "ix_payment_date");

            migrationBuilder.AddColumn<int>(
                name: "week_number",
                table: "reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_reservation_date",
                table: "reservations",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "ix_reservation_year_weeknumber_spacerateid",
                table: "reservations",
                columns: new[] { "year", "week_number", "space_rate_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_reservation_date",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "ix_reservation_year_weeknumber_spacerateid",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "week_number",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "year",
                table: "reservations");

            migrationBuilder.RenameIndex(
                name: "ix_payment_date",
                table: "payments",
                newName: "ix_payment_paymentdate");
        }
    }
}
