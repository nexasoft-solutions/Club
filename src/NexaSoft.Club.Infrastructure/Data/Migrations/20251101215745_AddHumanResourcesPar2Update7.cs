using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payroll_concepts_payroll_type_payroll_type_id",
                table: "payroll_concepts");

            migrationBuilder.DropIndex(
                name: "ix_payrollconcept_payrolltypeid",
                table: "payroll_concepts");

            migrationBuilder.DropColumn(
                name: "payroll_type_id",
                table: "payroll_concepts");

            migrationBuilder.CreateTable(
                name: "payroll_concept_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_concept_id = table.Column<long>(type: "bigint", nullable: false),
                    payroll_type_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_concept_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_concept_types_payroll_concepts_payroll_concept_id",
                        column: x => x.payroll_concept_id,
                        principalTable: "payroll_concepts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concept_types_payroll_type_payroll_type_id",
                        column: x => x.payroll_type_id,
                        principalTable: "payroll_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_payrollconceptid",
                table: "payroll_concept_types",
                column: "payroll_concept_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_payrolltypeid",
                table: "payroll_concept_types",
                column: "payroll_type_id");

            migrationBuilder.CreateIndex(
                name: "ux_payrollconcepttype_concept_type",
                table: "payroll_concept_types",
                columns: new[] { "payroll_concept_id", "payroll_type_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payroll_concept_types");

            migrationBuilder.AddColumn<long>(
                name: "payroll_type_id",
                table: "payroll_concepts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_payrolltypeid",
                table: "payroll_concepts",
                column: "payroll_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payroll_concepts_payroll_type_payroll_type_id",
                table: "payroll_concepts",
                column: "payroll_type_id",
                principalTable: "payroll_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
