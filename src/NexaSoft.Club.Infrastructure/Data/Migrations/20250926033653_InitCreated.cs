using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "accounting_charts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    account_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    account_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    parent_account_id = table.Column<long>(type: "bigint", nullable: true),
                    level = table.Column<int>(type: "integer", nullable: false),
                    allows_transactions = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    state_accounting_chart = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounting_charts", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounting_charts_accounting_charts_parent_account_id",
                        column: x => x.parent_account_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "accounting_entries",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    entry_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    entry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    source_module = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    source_id = table.Column<long>(type: "bigint", nullable: true),
                    total_debit = table.Column<decimal>(type: "numeric", nullable: false),
                    total_credit = table.Column<decimal>(type: "numeric", nullable: false),
                    is_adjusted = table.Column<bool>(type: "boolean", nullable: false),
                    adjustment_reason = table.Column<string>(type: "text", nullable: true),
                    state_accounting_entry = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounting_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "constantes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_constante = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    clave = table.Column<int>(type: "integer", nullable: false),
                    valor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estado_constante = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constantes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contadores",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    entidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    prefijo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    valor_actual = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    agrupador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tipo_dato = table.Column<string>(type: "text", nullable: true),
                    valor_rpeticion = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contadores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "member_statuses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    can_access = table.Column<bool>(type: "boolean", nullable: false),
                    can_reserve = table.Column<bool>(type: "boolean", nullable: false),
                    can_participate = table.Column<bool>(type: "boolean", nullable: false),
                    state_member_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "member_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    has_family_discount = table.Column<bool>(type: "boolean", nullable: true),
                    family_discount_rate = table.Column<decimal>(type: "numeric", nullable: true),
                    max_family_members = table.Column<int>(type: "integer", nullable: true),
                    state_member_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_item_options",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    route = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    estado_menu = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_item_options_menu_item_options_parent_id",
                        column: x => x.parent_id,
                        principalTable: "menu_item_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "periodicities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_periodicity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_periodicities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    referencia_control = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "system_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    state_system_user = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_system_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ubigeos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    nivel = table.Column<int>(type: "integer", nullable: false),
                    padre_id = table.Column<long>(type: "bigint", nullable: true),
                    estado_ubigeo = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ubigeos", x => x.id);
                    table.ForeignKey(
                        name: "fk_ubigeos_ubigeos_padre_id",
                        column: x => x.padre_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_apellidos = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    user_nombres = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    nombre_completo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    user_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    password = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    user_dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    user_telefono = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    estado_user = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "spaces",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    space_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    space_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    standard_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    requires_approval = table.Column<bool>(type: "boolean", nullable: false),
                    max_reservation_hours = table.Column<int>(type: "integer", nullable: false),
                    income_account_id = table.Column<long>(type: "bigint", nullable: true),
                    state_space = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spaces", x => x.id);
                    table.ForeignKey(
                        name: "fk_spaces_accounting_charts_income_account_id",
                        column: x => x.income_account_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "accounting_entry_items",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accounting_entry_id = table.Column<long>(type: "bigint", nullable: false),
                    accounting_chart_id = table.Column<long>(type: "bigint", nullable: false),
                    debit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    credit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_entry_item = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounting_entry_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounting_entry_items_accounting_charts",
                        column: x => x.accounting_chart_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_accounting_entry_items_accounting_entries",
                        column: x => x.accounting_entry_id,
                        principalTable: "accounting_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entry_items",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accounting_entry_id = table.Column<long>(type: "bigint", nullable: false),
                    accounting_chart_id = table.Column<long>(type: "bigint", nullable: false),
                    debit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    credit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    state_entry_item = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entry_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_entry_items_accounting_charts_accounting_chart_id",
                        column: x => x.accounting_chart_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_entry_items_accounting_entries_accounting_entry_id",
                        column: x => x.accounting_entry_id,
                        principalTable: "accounting_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "expenses_vouchers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accounting_entry_id = table.Column<long>(type: "bigint", nullable: false),
                    voucher_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    supplier_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    issue_date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    expense_account_id = table.Column<long>(type: "bigint", nullable: false),
                    state_expense_voucher = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_expenses_vouchers", x => x.id);
                    table.ForeignKey(
                        name: "fk_expenses_vouchers_accounting_charts_expense_account_id",
                        column: x => x.expense_account_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_expenses_vouchers_accounting_entries_accounting_entry_id",
                        column: x => x.accounting_entry_id,
                        principalTable: "accounting_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    member_type_id = table.Column<long>(type: "bigint", nullable: false),
                    member_status_id = table.Column<long>(type: "bigint", nullable: false),
                    join_date = table.Column<DateOnly>(type: "date", nullable: false),
                    expiration_date = table.Column<DateOnly>(type: "date", nullable: true),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    qr_code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    qr_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    profile_picture_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    state_member = table.Column<int>(type: "integer", nullable: false),
                    entry_fee_paid = table.Column<bool>(type: "boolean", nullable: false),
                    last_payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_members_member_status_member_status_id",
                        column: x => x.member_status_id,
                        principalTable: "member_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_members_member_type_member_type_id",
                        column: x => x.member_type_id,
                        principalTable: "member_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "menu_roles",
                columns: table => new
                {
                    menu_item_option_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_roles", x => new { x.menu_item_option_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_menu_roles_menu_item_options_menu_item_option_id",
                        column: x => x.menu_item_option_id,
                        principalTable: "menu_item_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fee_configurations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fee_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    periodicity_id = table.Column<long>(type: "bigint", nullable: false),
                    due_day = table.Column<int>(type: "integer", nullable: true),
                    default_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    is_variable = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    applies_to_family = table.Column<bool>(type: "boolean", nullable: false),
                    income_account_id = table.Column<long>(type: "bigint", nullable: true),
                    state_fee_configuration = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fee_configurations", x => x.id);
                    table.ForeignKey(
                        name: "fk_fee_configurations_accounting_charts_income_account_id",
                        column: x => x.income_account_id,
                        principalTable: "accounting_charts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_fee_configurations_periodicity_periodicity_id",
                        column: x => x.periodicity_id,
                        principalTable: "periodicities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    permission_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "space_rates",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    space_id = table.Column<long>(type: "bigint", nullable: false),
                    member_type_id = table.Column<long>(type: "bigint", nullable: false),
                    rate = table.Column<decimal>(type: "numeric", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    state_space_rate = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_space_rates", x => x.id);
                    table.ForeignKey(
                        name: "fk_space_rates_member_types_member_type_id",
                        column: x => x.member_type_id,
                        principalTable: "member_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_space_rates_spaces_space_id",
                        column: x => x.space_id,
                        principalTable: "spaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "family_members",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    dni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    relationship = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_authorized = table.Column<bool>(type: "boolean", nullable: false),
                    state_family_member = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_family_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_family_members_member_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    space_id = table.Column<long>(type: "bigint", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    accounting_entry_id = table.Column<long>(type: "bigint", nullable: true),
                    state_reservation = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.id);
                    table.ForeignKey(
                        name: "fk_reservations_accounting_entries_accounting_entry_id",
                        column: x => x.accounting_entry_id,
                        principalTable: "accounting_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reservations_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reservations_space_space_id",
                        column: x => x.space_id,
                        principalTable: "spaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member_fees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    fee_configuration_id = table.Column<long>(type: "bigint", nullable: true),
                    period = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    paid_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    remaining_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    state_member_fee = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_fees", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_fees_fee_configurations_fee_configuration_id",
                        column: x => x.fee_configuration_id,
                        principalTable: "fee_configurations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_fees_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member_types_fees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_type_id = table.Column<long>(type: "bigint", nullable: false),
                    fee_configuration_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_types_fees", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_types_fees_fee_configurations_fee_configuration_id",
                        column: x => x.fee_configuration_id,
                        principalTable: "fee_configurations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_types_fees_member_types_member_type_id",
                        column: x => x.member_type_id,
                        principalTable: "member_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    member_fee_id = table.Column<long>(type: "bigint", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    reference_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    receipt_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_partial = table.Column<bool>(type: "boolean", nullable: false),
                    accounting_entry_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payment = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_accounting_entries_accounting_entry_id",
                        column: x => x.accounting_entry_id,
                        principalTable: "accounting_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payments_member_fees_member_fee_id",
                        column: x => x.member_fee_id,
                        principalTable: "member_fees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payments_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_accounting_charts_parent_account_id",
                table: "accounting_charts",
                column: "parent_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_accountingchart_accountcode",
                table: "accounting_charts",
                column: "account_code");

            migrationBuilder.CreateIndex(
                name: "ix_accountingchart_accounttype",
                table: "accounting_charts",
                column: "account_type");

            migrationBuilder.CreateIndex(
                name: "ix_accountingchart_allowstransactions",
                table: "accounting_charts",
                column: "allows_transactions");

            migrationBuilder.CreateIndex(
                name: "ix_accountingchart_level",
                table: "accounting_charts",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "idx_accounting_entries_source",
                table: "accounting_entries",
                columns: new[] { "source_module", "source_id" });

            migrationBuilder.CreateIndex(
                name: "ix_accountingentry_entrydate",
                table: "accounting_entries",
                column: "entry_date");

            migrationBuilder.CreateIndex(
                name: "ix_accountingentry_entrynumber",
                table: "accounting_entries",
                column: "entry_number");

            migrationBuilder.CreateIndex(
                name: "ix_accountingentry_sourcemodule",
                table: "accounting_entries",
                column: "source_module");

            migrationBuilder.CreateIndex(
                name: "ix_accounting_entry_items_chart_id",
                table: "accounting_entry_items",
                column: "accounting_chart_id");

            migrationBuilder.CreateIndex(
                name: "ix_accounting_entry_items_entry_id",
                table: "accounting_entry_items",
                column: "accounting_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_constante_clave_valor",
                table: "constantes",
                columns: new[] { "clave", "valor" });

            migrationBuilder.CreateIndex(
                name: "ix_constante_tipoconstante",
                table: "constantes",
                column: "tipo_constante");

            migrationBuilder.CreateIndex(
                name: "ix_contador_entidad_prefijo",
                table: "contadores",
                columns: new[] { "entidad", "prefijo" });

            migrationBuilder.CreateIndex(
                name: "ix_entryitem_accountid",
                table: "entry_items",
                column: "accounting_chart_id");

            migrationBuilder.CreateIndex(
                name: "ix_entryitem_entryid",
                table: "entry_items",
                column: "accounting_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_vouchers_accounting_entry_id",
                table: "expenses_vouchers",
                column: "accounting_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_expenses_vouchers_expense_account_id",
                table: "expenses_vouchers",
                column: "expense_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_expensevoucher_issuedate",
                table: "expenses_vouchers",
                column: "issue_date");

            migrationBuilder.CreateIndex(
                name: "ix_expensevoucher_vouchernumber",
                table: "expenses_vouchers",
                column: "voucher_number");

            migrationBuilder.CreateIndex(
                name: "ix_familymember_dni",
                table: "family_members",
                column: "dni");

            migrationBuilder.CreateIndex(
                name: "ix_familymember_memberid",
                table: "family_members",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_familymember_relationship",
                table: "family_members",
                column: "relationship");

            migrationBuilder.CreateIndex(
                name: "ix_fee_configurations_income_account_id",
                table: "fee_configurations",
                column: "income_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_feeconfiguration_feename",
                table: "fee_configurations",
                column: "fee_name");

            migrationBuilder.CreateIndex(
                name: "ix_feeconfiguration_periodicity",
                table: "fee_configurations",
                column: "periodicity_id");

            migrationBuilder.CreateIndex(
                name: "idx_member_fees_member_status",
                table: "member_fees",
                columns: new[] { "member_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_memberfee_configid",
                table: "member_fees",
                column: "fee_configuration_id");

            migrationBuilder.CreateIndex(
                name: "ix_memberfee_memberid",
                table: "member_fees",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_memberfee_period",
                table: "member_fees",
                column: "period");

            migrationBuilder.CreateIndex(
                name: "ix_memberfee_status",
                table: "member_fees",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_memberstatus_statusname",
                table: "member_statuses",
                column: "status_name");

            migrationBuilder.CreateIndex(
                name: "ix_membertype_typename",
                table: "member_types",
                column: "type_name");

            migrationBuilder.CreateIndex(
                name: "ix_member_types_fees_fee_configuration_id",
                table: "member_types_fees",
                column: "fee_configuration_id");

            migrationBuilder.CreateIndex(
                name: "ix_membertype_fee_configuration",
                table: "member_types_fees",
                columns: new[] { "member_type_id", "fee_configuration_id" });

            migrationBuilder.CreateIndex(
                name: "ix_membertype_id",
                table: "member_types_fees",
                column: "member_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_dni",
                table: "members",
                column: "dni");

            migrationBuilder.CreateIndex(
                name: "ix_member_email",
                table: "members",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "ix_member_membertypeid",
                table: "members",
                column: "member_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_member_qrcode",
                table: "members",
                column: "qr_code");

            migrationBuilder.CreateIndex(
                name: "ix_member_statusid",
                table: "members",
                column: "member_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_options_parent_id",
                table: "menu_item_options",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_label",
                table: "menu_item_options",
                column: "label");

            migrationBuilder.CreateIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "idx_payments_member_date",
                table: "payments",
                columns: new[] { "member_id", "payment_date" });

            migrationBuilder.CreateIndex(
                name: "ix_payment_memberid",
                table: "payments",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_paymentdate",
                table: "payments",
                column: "payment_date");

            migrationBuilder.CreateIndex(
                name: "ix_payment_paymentmethod",
                table: "payments",
                column: "payment_method");

            migrationBuilder.CreateIndex(
                name: "ix_payment_receiptnumber",
                table: "payments",
                column: "receipt_number");

            migrationBuilder.CreateIndex(
                name: "ix_payments_accounting_entry_id",
                table: "payments",
                column: "accounting_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_member_fee_id",
                table: "payments",
                column: "member_fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_periodicity_name",
                table: "periodicities",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_permission_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_referenciacontrol",
                table: "permissions",
                column: "referencia_control");

            migrationBuilder.CreateIndex(
                name: "ix_expires",
                table: "refresh_tokens",
                column: "expires");

            migrationBuilder.CreateIndex(
                name: "ix_token",
                table: "refresh_tokens",
                column: "token");

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservation_accountingentryid",
                table: "reservations",
                column: "accounting_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservation_memberid",
                table: "reservations",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservation_spaceid",
                table: "reservations",
                column: "space_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservation_starttime",
                table: "reservations",
                column: "start_time");

            migrationBuilder.CreateIndex(
                name: "ix_reservation_status",
                table: "reservations",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_rolepermissions_permissionid",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_spacerate_isactive",
                table: "space_rates",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_spacerate_membertypeid",
                table: "space_rates",
                column: "member_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_spacerate_spaceid",
                table: "space_rates",
                column: "space_id");

            migrationBuilder.CreateIndex(
                name: "ix_space_isactive",
                table: "spaces",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_space_spacename",
                table: "spaces",
                column: "space_name");

            migrationBuilder.CreateIndex(
                name: "ix_space_spacetype",
                table: "spaces",
                column: "space_type");

            migrationBuilder.CreateIndex(
                name: "ix_spaces_income_account_id",
                table: "spaces",
                column: "income_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_systemuser_email",
                table: "system_users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "ix_systemuser_isactive",
                table: "system_users",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_systemuser_role",
                table: "system_users",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "ix_systemuser_username",
                table: "system_users",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_descripcion",
                table: "ubigeos",
                column: "descripcion");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_nivel",
                table: "ubigeos",
                column: "nivel");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_nivel_descripcion",
                table: "ubigeos",
                columns: new[] { "nivel", "descripcion" });

            migrationBuilder.CreateIndex(
                name: "ix_ubigeos_padre_id",
                table: "ubigeos",
                column: "padre_id");

            migrationBuilder.CreateIndex(
                name: "ix_userroles_roleid",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_nombrecompleto",
                table: "users",
                column: "nombre_completo");

            migrationBuilder.CreateIndex(
                name: "ix_user_userdni",
                table: "users",
                column: "user_dni");

            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "users",
                column: "user_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounting_entry_items");

            migrationBuilder.DropTable(
                name: "constantes");

            migrationBuilder.DropTable(
                name: "contadores");

            migrationBuilder.DropTable(
                name: "entry_items");

            migrationBuilder.DropTable(
                name: "expenses_vouchers");

            migrationBuilder.DropTable(
                name: "family_members");

            migrationBuilder.DropTable(
                name: "member_types_fees");

            migrationBuilder.DropTable(
                name: "menu_roles");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "space_rates");

            migrationBuilder.DropTable(
                name: "system_users");

            migrationBuilder.DropTable(
                name: "ubigeos");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "menu_item_options");

            migrationBuilder.DropTable(
                name: "member_fees");

            migrationBuilder.DropTable(
                name: "accounting_entries");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "spaces");

            migrationBuilder.DropTable(
                name: "fee_configurations");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "accounting_charts");

            migrationBuilder.DropTable(
                name: "periodicities");

            migrationBuilder.DropTable(
                name: "member_statuses");

            migrationBuilder.DropTable(
                name: "member_types");
        }
    }
}
