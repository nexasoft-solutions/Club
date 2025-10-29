using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "company_id",
                table: "employees_info",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "attendance_records",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<long>(type: "bigint", nullable: true),
                    cost_center_id = table.Column<long>(type: "bigint", nullable: true),
                    record_date = table.Column<DateOnly>(type: "date", nullable: false),
                    check_in_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    check_out_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    total_hours = table.Column<decimal>(type: "numeric", nullable: true),
                    regular_hours = table.Column<decimal>(type: "numeric", nullable: true),
                    overtime_hours = table.Column<decimal>(type: "numeric", nullable: true),
                    sunday_hours = table.Column<decimal>(type: "numeric", nullable: true),
                    holiday_hours = table.Column<decimal>(type: "numeric", nullable: true),
                    night_hours = table.Column<decimal>(type: "numeric", nullable: true),
                    late_minutes = table.Column<int>(type: "integer", nullable: true),
                    early_departure_minutes = table.Column<int>(type: "integer", nullable: true),
                    attendance_status_type_id = table.Column<long>(type: "bigint", nullable: true),
                    state_attendance_record = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendance_records", x => x.id);
                    table.ForeignKey(
                        name: "fk_attendance_records_attendance_status_type_attendance_status",
                        column: x => x.attendance_status_type_id,
                        principalTable: "attendance_status_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attendance_records_cost_center_cost_center_id",
                        column: x => x.cost_center_id,
                        principalTable: "cost_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attendance_records_employee_info_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ruc = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    business_name = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    trade_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    phone = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    website = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    state_company = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "concept_type_payrolls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    state_concept_type_payroll = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_concept_type_payrolls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employment_contracts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<long>(type: "bigint", nullable: true),
                    contract_type_id = table.Column<long>(type: "bigint", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    salary = table.Column<decimal>(type: "numeric", nullable: false),
                    working_hours = table.Column<int>(type: "integer", nullable: false),
                    document_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    state_employment_contract = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employment_contracts", x => x.id);
                    table.ForeignKey(
                        name: "fk_employment_contracts_contract_types_contract_type_id",
                        column: x => x.contract_type_id,
                        principalTable: "contract_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_employment_contracts_employees_info_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cost_center_id = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    expense_date = table.Column<DateOnly>(type: "date", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    document_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    document_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    state_expense = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expenses", x => x.id);
                    table.ForeignKey(
                        name: "fk_expenses_cost_centers_cost_center_id",
                        column: x => x.cost_center_id,
                        principalTable: "cost_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_formulas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    formula_expression = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    variables = table.Column<string>(type: "jsonb", nullable: true),
                    state_payroll_formula = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_formulas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_periods",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    period_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_employees = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payroll_period = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_periods", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_periods_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rate_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    state_rate_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rate_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time_requests",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<long>(type: "bigint", nullable: true),
                    time_request_type_id = table.Column<long>(type: "bigint", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    total_days = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    status_id = table.Column<long>(type: "bigint", nullable: true),
                    state_time_request = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_time_requests_employees_info_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_time_requests_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_time_requests_time_request_type_time_request_type_id",
                        column: x => x.time_request_type_id,
                        principalTable: "time_request_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "work_schedules",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<long>(type: "bigint", nullable: true),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    state_work_schedule = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_schedules_employees_info_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "company_bank_accounts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: true),
                    bank_id = table.Column<long>(type: "bigint", nullable: true),
                    bank_account_type_id = table.Column<long>(type: "bigint", nullable: true),
                    bank_account_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    cci_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    currency_id = table.Column<long>(type: "bigint", nullable: true),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    state_company_bank_account = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company_bank_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_company_bank_accounts_bank_account_types_bank_account_type_",
                        column: x => x.bank_account_type_id,
                        principalTable: "bank_account_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_company_bank_accounts_banks_bank_id",
                        column: x => x.bank_id,
                        principalTable: "banks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_company_bank_accounts_company_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_company_bank_accounts_currency_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_configs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: true),
                    pay_period_type_id = table.Column<long>(type: "bigint", nullable: true),
                    regular_hours_per_day = table.Column<decimal>(type: "numeric", nullable: false),
                    work_days_per_week = table.Column<int>(type: "integer", nullable: false),
                    state_payroll_config = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_configs", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_configs_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_configs_pay_period_types_pay_period_type_id",
                        column: x => x.pay_period_type_id,
                        principalTable: "pay_period_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_concepts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    concept_type_payroll_id = table.Column<long>(type: "bigint", nullable: true),
                    payroll_formula_id = table.Column<long>(type: "bigint", nullable: true),
                    concept_calculation_type_id = table.Column<long>(type: "bigint", nullable: true),
                    fixed_value = table.Column<decimal>(type: "numeric", nullable: false),
                    porcentaje_value = table.Column<decimal>(type: "numeric", nullable: false),
                    concept_application_types_id = table.Column<long>(type: "bigint", nullable: true),
                    accounting_chart_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payroll_concept = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_concepts", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_concepts_accounting_charts_accounting_chart_id",
                        column: x => x.accounting_chart_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concepts_concept_application_types_concept_applicat",
                        column: x => x.concept_application_types_id,
                        principalTable: "concept_application_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concepts_concept_calculation_types_concept_calculat",
                        column: x => x.concept_calculation_type_id,
                        principalTable: "concept_calculation_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concepts_concept_type_payrolls_concept_type_payroll",
                        column: x => x.concept_type_payroll_id,
                        principalTable: "concept_type_payrolls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_concepts_payroll_formula_payroll_formula_id",
                        column: x => x.payroll_formula_id,
                        principalTable: "payroll_formulas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_period_id = table.Column<long>(type: "bigint", nullable: false),
                    employee_id = table.Column<long>(type: "bigint", nullable: false),
                    cost_center_id = table.Column<long>(type: "bigint", nullable: true),
                    base_salary = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    total_income = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    total_deductions = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    net_pay = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    status_id = table.Column<long>(type: "bigint", nullable: true),
                    calculated_at = table.Column<DateOnly>(type: "date", nullable: true),
                    paid_at = table.Column<DateOnly>(type: "date", nullable: true),
                    state_payroll_detail = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_details_cost_centers_cost_center_id",
                        column: x => x.cost_center_id,
                        principalTable: "cost_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_details_employees_info_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_details_payroll_period_payroll_period_id",
                        column: x => x.payroll_period_id,
                        principalTable: "payroll_periods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_details_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "special_rates",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rate_type_id = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    multiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    state_special_rate = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_special_rates", x => x.id);
                    table.ForeignKey(
                        name: "fk_special_rates_rate_types_rate_type_id",
                        column: x => x.rate_type_id,
                        principalTable: "rate_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payroll_detail_concepts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_detail_id = table.Column<long>(type: "bigint", nullable: false),
                    concept_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    calculated_value = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    state_payroll_detail_concept = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_detail_concepts", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_detail_concepts_payroll_concepts_concept_id",
                        column: x => x.concept_id,
                        principalTable: "payroll_concepts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_detail_concepts_payroll_detail_payroll_detail_id",
                        column: x => x.payroll_detail_id,
                        principalTable: "payroll_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payroll_payment_records",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payroll_detail_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    currency_id = table.Column<long>(type: "bigint", nullable: false),
                    exchange_rate = table.Column<decimal>(type: "numeric(8,4)", nullable: false, defaultValue: 1.0m),
                    reference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    bank_id = table.Column<long>(type: "bigint", nullable: true),
                    company_bank_account_id = table.Column<long>(type: "bigint", nullable: true),
                    status_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_file_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    confirmation_document_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    processed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    processed_by_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payroll_payment_record = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payroll_payment_records", x => x.id);
                    table.ForeignKey(
                        name: "fk_payroll_payment_records_banks_bank_id",
                        column: x => x.bank_id,
                        principalTable: "banks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_payment_records_company_bank_accounts_company_bank_",
                        column: x => x.company_bank_account_id,
                        principalTable: "company_bank_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_payment_records_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_payment_records_payment_method_types_payment_method",
                        column: x => x.payment_method_id,
                        principalTable: "payment_method_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_payment_records_payroll_details_payroll_detail_id",
                        column: x => x.payroll_detail_id,
                        principalTable: "payroll_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payroll_payment_records_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_info_company_id",
                table: "employees_info",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendancerecord_attendancestatustypeid",
                table: "attendance_records",
                column: "attendance_status_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendancerecord_costcenterid",
                table: "attendance_records",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendancerecord_employeeid",
                table: "attendance_records",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "unique_attendance",
                table: "attendance_records",
                columns: new[] { "employee_id", "record_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_company_businessname",
                table: "companies",
                column: "business_name");

            migrationBuilder.CreateIndex(
                name: "ix_company_ruc",
                table: "companies",
                column: "ruc");

            migrationBuilder.CreateIndex(
                name: "ix_companybankaccount_bankaccounttypeid",
                table: "company_bank_accounts",
                column: "bank_account_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_companybankaccount_bankid",
                table: "company_bank_accounts",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "ix_companybankaccount_companyid",
                table: "company_bank_accounts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_companybankaccount_currencyid",
                table: "company_bank_accounts",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_concepttypepayroll_code",
                table: "concept_type_payrolls",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_concepttypepayroll_name",
                table: "concept_type_payrolls",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_employmentcontract_contracttypeid",
                table: "employment_contracts",
                column: "contract_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_employmentcontract_employeeid",
                table: "employment_contracts",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_expense_costcenterid",
                table: "expenses",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "ix_payroll_concepts_accounting_chart_id",
                table: "payroll_concepts",
                column: "accounting_chart_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_code",
                table: "payroll_concepts",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_conceptapplicationtypesid",
                table: "payroll_concepts",
                column: "concept_application_types_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_conceptcalculationtypeid",
                table: "payroll_concepts",
                column: "concept_calculation_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_concepttypepayrollid",
                table: "payroll_concepts",
                column: "concept_type_payroll_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_name",
                table: "payroll_concepts",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconcept_payrollformulaid",
                table: "payroll_concepts",
                column: "payroll_formula_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconfig_companyid",
                table: "payroll_configs",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollconfig_payperiodtypeid",
                table: "payroll_configs",
                column: "pay_period_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolldetailconcept_conceptid",
                table: "payroll_detail_concepts",
                column: "concept_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolldetailconcept_payrolldetailid",
                table: "payroll_detail_concepts",
                column: "payroll_detail_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolldetail_costcenterid",
                table: "payroll_details",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolldetail_employeeid",
                table: "payroll_details",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolldetail_payrollperiodid",
                table: "payroll_details",
                column: "payroll_period_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrolldetail_statusid",
                table: "payroll_details",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollformula_code",
                table: "payroll_formulas",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_payrollformula_formulaexpression",
                table: "payroll_formulas",
                column: "formula_expression");

            migrationBuilder.CreateIndex(
                name: "ix_payrollformula_name",
                table: "payroll_formulas",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_bankid",
                table: "payroll_payment_records",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_companybankaccountid",
                table: "payroll_payment_records",
                column: "company_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_currencyid",
                table: "payroll_payment_records",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_paymentmethodid",
                table: "payroll_payment_records",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_payrolldetailid",
                table: "payroll_payment_records",
                column: "payroll_detail_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_processedbyid",
                table: "payroll_payment_records",
                column: "processed_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollpaymentrecord_statusid",
                table: "payroll_payment_records",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_payrollperiod_periodname",
                table: "payroll_periods",
                column: "period_name");

            migrationBuilder.CreateIndex(
                name: "ix_payrollperiod_statusid",
                table: "payroll_periods",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_ratetype_code",
                table: "rate_types",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_ratetype_name",
                table: "rate_types",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_specialrate_ratetypeid",
                table: "special_rates",
                column: "rate_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_timerequest_employeeid",
                table: "time_requests",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_timerequest_statusid",
                table: "time_requests",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_timerequest_timerequesttypeid",
                table: "time_requests",
                column: "time_request_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_workschedule_employeeid",
                table: "work_schedules",
                column: "employee_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_info_companies_company_id",
                table: "employees_info",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_info_companies_company_id",
                table: "employees_info");

            migrationBuilder.DropTable(
                name: "attendance_records");

            migrationBuilder.DropTable(
                name: "employment_contracts");

            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "payroll_configs");

            migrationBuilder.DropTable(
                name: "payroll_detail_concepts");

            migrationBuilder.DropTable(
                name: "payroll_payment_records");

            migrationBuilder.DropTable(
                name: "special_rates");

            migrationBuilder.DropTable(
                name: "time_requests");

            migrationBuilder.DropTable(
                name: "work_schedules");

            migrationBuilder.DropTable(
                name: "payroll_concepts");

            migrationBuilder.DropTable(
                name: "company_bank_accounts");

            migrationBuilder.DropTable(
                name: "payroll_details");

            migrationBuilder.DropTable(
                name: "rate_types");

            migrationBuilder.DropTable(
                name: "concept_type_payrolls");

            migrationBuilder.DropTable(
                name: "payroll_formulas");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "payroll_periods");

            migrationBuilder.DropIndex(
                name: "ix_employees_info_company_id",
                table: "employees_info");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "employees_info");
        }
    }
}
