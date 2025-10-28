using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReservationUpdatedRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservations_space_rate_space_rate_id",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "space_rate_id",
                table: "reservations",
                newName: "space_id");

            migrationBuilder.RenameIndex(
                name: "ix_reservation_year_weeknumber_spacerateid",
                table: "reservations",
                newName: "ix_reservation_year_weeknumber_spaceid");

            migrationBuilder.RenameIndex(
                name: "ix_reservation_spacerateid",
                table: "reservations",
                newName: "ix_reservation_spaceid");

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_space_space_id",
                table: "reservations",
                column: "space_id",
                principalTable: "spaces",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservations_space_space_id",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "space_id",
                table: "reservations",
                newName: "space_rate_id");

            migrationBuilder.RenameIndex(
                name: "ix_reservation_year_weeknumber_spaceid",
                table: "reservations",
                newName: "ix_reservation_year_weeknumber_spacerateid");

            migrationBuilder.RenameIndex(
                name: "ix_reservation_spaceid",
                table: "reservations",
                newName: "ix_reservation_spacerateid");

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_space_rate_space_rate_id",
                table: "reservations",
                column: "space_rate_id",
                principalTable: "space_rates",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
