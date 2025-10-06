using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMemberMetrcis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "member_visits",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: true),
                    visit_date = table.Column<DateOnly>(type: "date", nullable: true),
                    entry_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    exit_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    qr_code_used = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    check_in_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    check_out_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    visit_type = table.Column<int>(type: "integer", nullable: false),
                    state_member_visit = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_visits", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_visits_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_membervisit_checkinby",
                table: "member_visits",
                column: "check_in_by");

            migrationBuilder.CreateIndex(
                name: "ix_membervisit_entrytime",
                table: "member_visits",
                column: "entry_time");

            migrationBuilder.CreateIndex(
                name: "ix_membervisit_exittime",
                table: "member_visits",
                column: "exit_time");

            migrationBuilder.CreateIndex(
                name: "ix_membervisit_memberid",
                table: "member_visits",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_membervisit_visitdate",
                table: "member_visits",
                column: "visit_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_visits");
        }
    }
}
