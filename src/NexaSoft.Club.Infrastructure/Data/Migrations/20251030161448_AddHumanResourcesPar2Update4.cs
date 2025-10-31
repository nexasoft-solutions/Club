using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ix_payroll_concepts_accounting_chart_id",
                table: "payroll_concepts",
                newName: "ix_payrollconcept_accountingchartid");

            migrationBuilder.AddColumn<long>(
                name: "payroll_type_id",
                table: "payroll_concepts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payroll_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    state_payroll_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_payrolltypeid",
                table: "payroll_concepts",
                column: "payroll_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolltype_code",
                table: "payroll_types",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_payrolltype_name",
                table: "payroll_types",
                column: "name");

            migrationBuilder.AddForeignKey(
                name: "fk_payroll_concepts_payroll_type_payroll_type_id",
                table: "payroll_concepts",
                column: "payroll_type_id",
                principalTable: "payroll_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payroll_concepts_payroll_type_payroll_type_id",
                table: "payroll_concepts");

            migrationBuilder.DropTable(
                name: "payroll_types");

            migrationBuilder.DropIndex(
                name: "ix_payrollconcept_payrolltypeid",
                table: "payroll_concepts");

            migrationBuilder.DropColumn(
                name: "payroll_type_id",
                table: "payroll_concepts");

            migrationBuilder.RenameIndex(
                name: "ix_payrollconcept_accountingchartid",
                table: "payroll_concepts",
                newName: "ix_payroll_concepts_accounting_chart_id");
        }
    }
}
