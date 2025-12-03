using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permission_referenciacontrol",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "referencia_control",
                table: "permissions");

            migrationBuilder.AddColumn<string>(
                name: "action",
                table: "permissions",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reference",
                table: "permissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "source",
                table: "permissions",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_permission_reference",
                table: "permissions",
                column: "reference");

            migrationBuilder.CreateIndex(
                name: "ix_permission_source",
                table: "permissions",
                column: "source");

            migrationBuilder.CreateIndex(
                name: "ix_permission_source_action",
                table: "permissions",
                columns: new[] { "source", "action" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permission_reference",
                table: "permissions");

            migrationBuilder.DropIndex(
                name: "ix_permission_source",
                table: "permissions");

            migrationBuilder.DropIndex(
                name: "ix_permission_source_action",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "action",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "source",
                table: "permissions");

            migrationBuilder.AddColumn<string>(
                name: "referencia_control",
                table: "permissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_permission_referenciacontrol",
                table: "permissions",
                column: "referencia_control");
        }
    }
}
