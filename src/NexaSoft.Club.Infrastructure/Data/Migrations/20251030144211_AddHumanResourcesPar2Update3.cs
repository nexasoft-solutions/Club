using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payroll_periods_status_status_id",
                table: "payroll_periods");

            migrationBuilder.CreateTable(
                name: "payroll_period_statuses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    state_payroll_period_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_period_statuses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payrollperiod_status_code",
                table: "payroll_period_statuses",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_payrollperiod_status_name",
                table: "payroll_period_statuses",
                column: "name");

            migrationBuilder.AddForeignKey(
                name: "fk_payroll_periods_payroll_period_status_status_id",
                table: "payroll_periods",
                column: "status_id",
                principalTable: "payroll_period_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payroll_periods_payroll_period_status_status_id",
                table: "payroll_periods");

            migrationBuilder.DropTable(
                name: "payroll_period_statuses");

            migrationBuilder.AddForeignKey(
                name: "fk_payroll_periods_status_status_id",
                table: "payroll_periods",
                column: "status_id",
                principalTable: "statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
