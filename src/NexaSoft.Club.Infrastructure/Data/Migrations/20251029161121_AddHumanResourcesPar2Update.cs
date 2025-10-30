using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_payrolls_tatus_types",
                table: "payrolls_tatus_types");

            migrationBuilder.RenameTable(
                name: "payrolls_tatus_types",
                newName: "payroll_status_types");

            migrationBuilder.AddColumn<bool>(
                name: "deducts_salary",
                table: "time_request_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "requires_approval",
                table: "time_request_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "end_date",
                table: "cost_centers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "pk_payroll_status_types",
                table: "payroll_status_types",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_payroll_status_types",
                table: "payroll_status_types");

            migrationBuilder.DropColumn(
                name: "deducts_salary",
                table: "time_request_types");

            migrationBuilder.DropColumn(
                name: "requires_approval",
                table: "time_request_types");

            migrationBuilder.RenameTable(
                name: "payroll_status_types",
                newName: "payrolls_tatus_types");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "end_date",
                table: "cost_centers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_payrolls_tatus_types",
                table: "payrolls_tatus_types",
                column: "id");
        }
    }
}
