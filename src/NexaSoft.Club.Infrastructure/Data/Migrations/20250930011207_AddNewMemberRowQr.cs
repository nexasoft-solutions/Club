using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMemberRowQr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "qr_expiration",
                table: "members",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qr_url",
                table: "members",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "member_qr_history",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    qr_code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    qr_url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    expiration_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_qr_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_qr_history_member_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_member_qrexpiration",
                table: "members",
                column: "qr_expiration");

            migrationBuilder.CreateIndex(
                name: "ix_member_status_qrexpiration",
                table: "members",
                columns: new[] { "status", "qr_expiration" });

            migrationBuilder.CreateIndex(
                name: "ix_memberqrhistory_expiration",
                table: "member_qr_history",
                column: "expiration_date");

            migrationBuilder.CreateIndex(
                name: "ix_memberqrhistory_memberid",
                table: "member_qr_history",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_memberqrhistory_qrcode",
                table: "member_qr_history",
                column: "qr_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_qr_history");

            migrationBuilder.DropIndex(
                name: "ix_member_qrexpiration",
                table: "members");

            migrationBuilder.DropIndex(
                name: "ix_member_status_qrexpiration",
                table: "members");

            migrationBuilder.DropColumn(
                name: "qr_url",
                table: "members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "qr_expiration",
                table: "members",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
