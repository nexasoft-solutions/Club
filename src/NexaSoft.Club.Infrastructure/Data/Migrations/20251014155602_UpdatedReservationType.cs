using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReservationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservations_document_types_document_type_id",
                table: "reservations");

            migrationBuilder.DropForeignKey(
                name: "fk_reservations_payment_types_payment_type_id",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "ix_reservations_payment_type_id",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "payment_type_id",
                table: "reservations");

            migrationBuilder.AlterColumn<string>(
                name: "reference_number",
                table: "reservations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "receipt_number",
                table: "reservations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_reservations_payment_method_id",
                table: "reservations",
                column: "payment_method_id");

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_document_types_document_type_id",
                table: "reservations",
                column: "document_type_id",
                principalTable: "document_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_payment_types_payment_method_id",
                table: "reservations",
                column: "payment_method_id",
                principalTable: "payment_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservations_document_types_document_type_id",
                table: "reservations");

            migrationBuilder.DropForeignKey(
                name: "fk_reservations_payment_types_payment_method_id",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "ix_reservations_payment_method_id",
                table: "reservations");

            migrationBuilder.AlterColumn<string>(
                name: "reference_number",
                table: "reservations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "receipt_number",
                table: "reservations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "payment_type_id",
                table: "reservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_reservations_payment_type_id",
                table: "reservations",
                column: "payment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_document_types_document_type_id",
                table: "reservations",
                column: "document_type_id",
                principalTable: "document_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reservations_payment_types_payment_type_id",
                table: "reservations",
                column: "payment_type_id",
                principalTable: "payment_types",
                principalColumn: "id");
        }
    }
}
