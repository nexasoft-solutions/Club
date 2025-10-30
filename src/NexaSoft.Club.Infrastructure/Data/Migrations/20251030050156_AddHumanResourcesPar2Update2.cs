using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "cost_center_id",
                table: "employees_info",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "income_tax_scales",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scale_year = table.Column<int>(type: "integer", nullable: false),
                    min_income = table.Column<decimal>(type: "numeric", nullable: false),
                    max_income = table.Column<decimal>(type: "numeric", nullable: true),
                    fixed_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    rate = table.Column<decimal>(type: "numeric", nullable: false),
                    excess_over = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    state_income_tax_scale = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_income_tax_scales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_parameters",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    value_text = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    effective_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    state_legal_parameter = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_legal_parameters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_concept_departments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_concept_id = table.Column<long>(type: "bigint", nullable: true),
                    department_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payroll_concept_department = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_concept_departments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_concept_departments_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concept_departments_payroll_concepts_payroll_concep",
                        column: x => x.payroll_concept_id,
                        principalTable: "payroll_concepts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_concept_employee_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_concept_id = table.Column<long>(type: "bigint", nullable: true),
                    employee_type_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payroll_concept_employee_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_concept_employee_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_concept_employee_types_employee_types_employee_type",
                        column: x => x.employee_type_id,
                        principalTable: "employee_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concept_employee_types_payroll_concepts_payroll_con",
                        column: x => x.payroll_concept_id,
                        principalTable: "payroll_concepts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_concept_employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_concept_id = table.Column<long>(type: "bigint", nullable: true),
                    employee_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payroll_concept_employee = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_concept_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_concept_employees_employees_info_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concept_employees_payroll_concepts_payroll_concept_",
                        column: x => x.payroll_concept_id,
                        principalTable: "payroll_concepts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tax_rates",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    rate_value = table.Column<decimal>(type: "numeric", nullable: false),
                    rate_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    min_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    max_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    effective_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    applies_to = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    state_tax_rate = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tax_rates", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_info_cost_center_id",
                table: "employees_info",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "ix_incometaxscale_scaleyear",
                table: "income_tax_scales",
                column: "scale_year");

            migrationBuilder.CreateIndex(
                name: "ix_legal_parameters_dates",
                table: "legal_parameters",
                columns: new[] { "effective_date", "end_date" });

            migrationBuilder.CreateIndex(
                name: "ix_legalparameter_category",
                table: "legal_parameters",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_legalparameter_code",
                table: "legal_parameters",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_legalparameter_name",
                table: "legal_parameters",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconceptdepartment_departmentid",
                table: "payroll_concept_departments",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconceptdepartment_payrollconceptid",
                table: "payroll_concept_departments",
                column: "payroll_concept_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconceptemployeetype_employeetypeid",
                table: "payroll_concept_employee_types",
                column: "employee_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconceptemployeetype_payrollconceptid",
                table: "payroll_concept_employee_types",
                column: "payroll_concept_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconceptemployee_employeeid",
                table: "payroll_concept_employees",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconceptemployee_payrollconceptid",
                table: "payroll_concept_employees",
                column: "payroll_concept_id");

            migrationBuilder.CreateIndex(
                name: "ix_taxrate_category",
                table: "tax_rates",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_taxrate_code",
                table: "tax_rates",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_taxrate_name",
                table: "tax_rates",
                column: "name");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_info_cost_centers_cost_center_id",
                table: "employees_info",
                column: "cost_center_id",
                principalTable: "cost_centers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_info_cost_centers_cost_center_id",
                table: "employees_info");

            migrationBuilder.DropTable(
                name: "income_tax_scales");

            migrationBuilder.DropTable(
                name: "legal_parameters");

            migrationBuilder.DropTable(
                name: "payroll_concept_departments");

            migrationBuilder.DropTable(
                name: "payroll_concept_employee_types");

            migrationBuilder.DropTable(
                name: "payroll_concept_employees");

            migrationBuilder.DropTable(
                name: "tax_rates");

            migrationBuilder.DropIndex(
                name: "ix_employees_info_cost_center_id",
                table: "employees_info");

            migrationBuilder.DropColumn(
                name: "cost_center_id",
                table: "employees_info");
        }
    }
}
