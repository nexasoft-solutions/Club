using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMemberToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "biometric_token",
                table: "members",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "device_id",
                table: "members",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_set_password",
                table: "members",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                table: "members",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "members",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "password_set_date",
                table: "members",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "member_refresh_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_revoked = table.Column<bool>(type: "boolean", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_refresh_tokens_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_expiresat",
                table: "member_refresh_tokens",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_isrevoked",
                table: "member_refresh_tokens",
                column: "is_revoked");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_memberid",
                table: "member_refresh_tokens",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_token",
                table: "member_refresh_tokens",
                column: "token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_refresh_tokens");

            migrationBuilder.DropColumn(
                name: "biometric_token",
                table: "members");

            migrationBuilder.DropColumn(
                name: "device_id",
                table: "members");

            migrationBuilder.DropColumn(
                name: "has_set_password",
                table: "members");

            migrationBuilder.DropColumn(
                name: "last_login_date",
                table: "members");

            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "members");

            migrationBuilder.DropColumn(
                name: "password_set_date",
                table: "members");
        }
    }
}
