using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservations_space_space_id",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "space_id",
                table: "reservations",
                newName: "space_rate_id");

            migrationBuilder.RenameIndex(
                name: "ix_reservation_spaceid",
                table: "reservations",
                newName: "ix_reservation_spacerateid");

            migrationBuilder.AddColumn<long>(
                name: "space_availability_id",
                table: "reservations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_reservations_space_availability_id",
                table: "reservations",
                column: "space_availability_id");

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_space_availability_space_availability_id",
                table: "reservations",
                column: "space_availability_id",
                principalTable: "space_availabilities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_space_rate_space_rate_id",
                table: "reservations",
                column: "space_rate_id",
                principalTable: "space_rates",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservations_space_availability_space_availability_id",
                table: "reservations");

            migrationBuilder.DropForeignKey(
                name: "fk_reservations_space_rate_space_rate_id",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "ix_reservations_space_availability_id",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "space_availability_id",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "space_rate_id",
                table: "reservations",
                newName: "space_id");

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
    }
}
