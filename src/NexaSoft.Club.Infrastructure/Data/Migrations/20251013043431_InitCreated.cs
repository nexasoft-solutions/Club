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
                name: "account_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_account_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_types", x => x.id);
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
                name: "document_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    serie = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    state_document_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_document_types", x => x.id);
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
                name: "payment_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_payment_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_types", x => x.id);
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
                name: "source_modules",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_source_module = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_source_modules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "space_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_space_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_space_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ubigeos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    level = table.Column<int>(type: "integer", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    state_ubigeo = table.Column<int>(type: "integer", nullable: false),
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
                        name: "fk_ubigeos_ubigeos_parent_id",
                        column: x => x.parent_id,
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
                name: "user_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    is_administrative = table.Column<bool>(type: "boolean", nullable: false),
                    state_user_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounting_charts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    account_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    account_type_id = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "fk_accounting_charts_account_type_account_type_id",
                        column: x => x.account_type_id,
                        principalTable: "account_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_accounting_charts_accounting_charts_parent_account_id",
                        column: x => x.parent_account_id,
                        principalTable: "accounting_charts",
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
                name: "accounting_entries",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    entry_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    entry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    source_module_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "fk_accounting_entries_source_module_source_module_id",
                        column: x => x.source_module_id,
                        principalTable: "source_modules",
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
                    departament_id = table.Column<long>(type: "bigint", nullable: false),
                    province_id = table.Column<long>(type: "bigint", nullable: false),
                    district_id = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    member_type_id = table.Column<long>(type: "bigint", nullable: false),
                    member_status_id = table.Column<long>(type: "bigint", nullable: false),
                    join_date = table.Column<DateOnly>(type: "date", nullable: false),
                    expiration_date = table.Column<DateOnly>(type: "date", nullable: true),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    state_member = table.Column<int>(type: "integer", nullable: false),
                    entry_fee_paid = table.Column<bool>(type: "boolean", nullable: false),
                    last_payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status_id = table.Column<long>(type: "bigint", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: true),
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
                    table.ForeignKey(
                        name: "fk_members_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_members_ubigeo_departament_id",
                        column: x => x.departament_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_members_ubigeo_district_id",
                        column: x => x.district_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_members_ubigeo_province_id",
                        column: x => x.province_id,
                        principalTable: "ubigeos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "spaces",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    space_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    space_type_id = table.Column<long>(type: "bigint", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    standard_rate = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.ForeignKey(
                        name: "fk_spaces_space_type_space_type_id",
                        column: x => x.space_type_id,
                        principalTable: "space_types",
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
                name: "member_pins",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    pin = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    device_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false),
                    used_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_pins", x => x.id);
                    table.ForeignKey(
                        name: "fk_member_pins_members",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false),
                    reference_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    document_type_id = table.Column<long>(type: "bigint", nullable: false),
                    receipt_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_partial = table.Column<bool>(type: "boolean", nullable: false),
                    accounting_entry_id = table.Column<long>(type: "bigint", nullable: true),
                    state_payment = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<long>(type: "bigint", nullable: false),
                    credit_balance = table.Column<decimal>(type: "numeric", nullable: false),
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
                        name: "fk_payments_document_types_document_type_id",
                        column: x => x.document_type_id,
                        principalTable: "document_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payments_payment_type_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payments_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    last_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    full_name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    user_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    has_set_password = table.Column<bool>(type: "boolean", nullable: false),
                    password = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    dni = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    phone = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    user_status = table.Column<int>(type: "integer", nullable: false),
                    password_set_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    biometric_token = table.Column<string>(type: "text", nullable: true),
                    device_id = table.Column<string>(type: "text", nullable: true),
                    qr_code = table.Column<string>(type: "text", nullable: true),
                    qr_expiration = table.Column<DateOnly>(type: "date", nullable: true),
                    qr_url = table.Column<string>(type: "text", nullable: true),
                    profile_picture_url = table.Column<string>(type: "text", nullable: true),
                    member_id = table.Column<long>(type: "bigint", nullable: true),
                    user_type_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "fk_users_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_users_user_type_user_type_id",
                        column: x => x.user_type_id,
                        principalTable: "user_types",
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
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    space_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    status_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_type_id = table.Column<long>(type: "bigint", nullable: true),
                    reference_number = table.Column<string>(type: "text", nullable: true),
                    document_type_id = table.Column<long>(type: "bigint", nullable: false),
                    receipt_number = table.Column<string>(type: "text", nullable: true),
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
                        name: "fk_reservations_document_types_document_type_id",
                        column: x => x.document_type_id,
                        principalTable: "document_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reservations_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reservations_payment_types_payment_type_id",
                        column: x => x.payment_type_id,
                        principalTable: "payment_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_reservations_space_space_id",
                        column: x => x.space_id,
                        principalTable: "spaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_reservations_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "space_availabilities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    space_id = table.Column<long>(type: "bigint", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    min_reservation_hours = table.Column<int>(type: "integer", nullable: false),
                    max_reservation_days_in_advance = table.Column<int>(type: "integer", nullable: false),
                    max_reservations_per_day = table.Column<int>(type: "integer", nullable: true),
                    state_space_availability = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_space_availabilities", x => x.id);
                    table.ForeignKey(
                        name: "fk_space_availabilities_space_space_id",
                        column: x => x.space_id,
                        principalTable: "spaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "space_photos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    space_id = table.Column<long>(type: "bigint", nullable: false),
                    photo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    state_space_photo = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_space_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_space_photos_spaces_space_id",
                        column: x => x.space_id,
                        principalTable: "spaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "user_qr_historys",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    qr_code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    qr_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("pk_user_qr_historys", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_qr_historys_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member_fees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    member_id = table.Column<long>(type: "bigint", nullable: false),
                    member_type_fee_id = table.Column<long>(type: "bigint", nullable: true),
                    period = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status_id = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "fk_member_fees_member_type_fee_member_type_fee_id",
                        column: x => x.member_type_fee_id,
                        principalTable: "member_types_fees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_fees_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_member_fees_status_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payment_items",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_id = table.Column<long>(type: "bigint", nullable: false),
                    member_fee_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    state_payment_item = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    deleted_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_items", x => x.id);
                    table.CheckConstraint("chk_paymentitem_amount_positive", "amount > 0");
                    table.ForeignKey(
                        name: "fk_payment_items_member_fees_member_fee_id",
                        column: x => x.member_fee_id,
                        principalTable: "member_fees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payment_items_payments_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_accounttype_name",
                table: "account_types",
                column: "name");

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
                column: "account_type_id");

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
                columns: new[] { "source_module_id", "source_id" });

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
                column: "source_module_id");

            migrationBuilder.CreateIndex(
                name: "ix_accounting_entry_items_chart_id",
                table: "accounting_entry_items",
                column: "accounting_chart_id");

            migrationBuilder.CreateIndex(
                name: "ix_accounting_entry_items_entry_id",
                table: "accounting_entry_items",
                column: "accounting_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_contador_entidad_prefijo",
                table: "contadores",
                columns: new[] { "entidad", "prefijo" });

            migrationBuilder.CreateIndex(
                name: "ix_documenttype_name",
                table: "document_types",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_documenttype_serie",
                table: "document_types",
                column: "serie");

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
                columns: new[] { "member_id", "status_id" });

            migrationBuilder.CreateIndex(
                name: "ix_memberfee_configid",
                table: "member_fees",
                column: "member_type_fee_id");

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
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_memberpins_expiresat",
                table: "member_pins",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "ix_memberpins_isused",
                table: "member_pins",
                column: "is_used");

            migrationBuilder.CreateIndex(
                name: "ix_memberpins_memberid",
                table: "member_pins",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_memberpins_pin",
                table: "member_pins",
                column: "pin");

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
                name: "ix_member_statusid",
                table: "members",
                column: "member_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_members_departament_id",
                table: "members",
                column: "departament_id");

            migrationBuilder.CreateIndex(
                name: "ix_members_district_id",
                table: "members",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_members_province_id",
                table: "members",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "ix_members_status_id",
                table: "members",
                column: "status_id");

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
                name: "idx_paymentitems_payment_fee",
                table: "payment_items",
                columns: new[] { "payment_id", "member_fee_id" },
                unique: true,
                filter: "deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_paymentitem_createdat",
                table: "payment_items",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_paymentitem_memberfeeid",
                table: "payment_items",
                column: "member_fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_paymentitem_paymentid",
                table: "payment_items",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "ix_paymentitem_state",
                table: "payment_items",
                column: "state_payment_item");

            migrationBuilder.CreateIndex(
                name: "ix_paymenttype_name",
                table: "payment_types",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "idx_payments_member_date",
                table: "payments",
                columns: new[] { "member_id", "payment_date" });

            migrationBuilder.CreateIndex(
                name: "ix_payment_accountingentryid",
                table: "payments",
                column: "accounting_entry_id");

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
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_receiptnumber",
                table: "payments",
                column: "receipt_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payments_document_type_id",
                table: "payments",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_status_id",
                table: "payments",
                column: "status_id");

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
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservations_document_type_id",
                table: "reservations",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservations_payment_type_id",
                table: "reservations",
                column: "payment_type_id");

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
                name: "ix_sourcemodule_name",
                table: "source_modules",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_spaceavailability_spaceid",
                table: "space_availabilities",
                column: "space_id");

            migrationBuilder.CreateIndex(
                name: "ix_spacephoto_spaceid",
                table: "space_photos",
                column: "space_id");

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
                name: "ix_spacetype_name",
                table: "space_types",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_space_spacename",
                table: "spaces",
                column: "space_name");

            migrationBuilder.CreateIndex(
                name: "ix_space_spacetype",
                table: "spaces",
                column: "space_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_spaces_income_account_id",
                table: "spaces",
                column: "income_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_status_name",
                table: "statuses",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_description",
                table: "ubigeos",
                column: "description");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_level",
                table: "ubigeos",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "ix_ubigeo_level_description",
                table: "ubigeos",
                columns: new[] { "level", "description" });

            migrationBuilder.CreateIndex(
                name: "ix_ubigeos_parent_id",
                table: "ubigeos",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_userqrhistory_expirationdate",
                table: "user_qr_historys",
                column: "expiration_date");

            migrationBuilder.CreateIndex(
                name: "ix_userqrhistory_qrcode",
                table: "user_qr_historys",
                column: "qr_code");

            migrationBuilder.CreateIndex(
                name: "ix_userqrhistory_userid",
                table: "user_qr_historys",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_userroles_roleid",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_usertype_isadministrative",
                table: "user_types",
                column: "is_administrative");

            migrationBuilder.CreateIndex(
                name: "ix_usertype_name",
                table: "user_types",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_fullname",
                table: "users",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "ix_user_userdni",
                table: "users",
                column: "dni");

            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "users",
                column: "user_name");

            migrationBuilder.CreateIndex(
                name: "ix_users_member_id",
                table: "users",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_type_id",
                table: "users",
                column: "user_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounting_entry_items");

            migrationBuilder.DropTable(
                name: "contadores");

            migrationBuilder.DropTable(
                name: "entry_items");

            migrationBuilder.DropTable(
                name: "expenses_vouchers");

            migrationBuilder.DropTable(
                name: "family_members");

            migrationBuilder.DropTable(
                name: "member_pins");

            migrationBuilder.DropTable(
                name: "member_refresh_tokens");

            migrationBuilder.DropTable(
                name: "member_visits");

            migrationBuilder.DropTable(
                name: "menu_roles");

            migrationBuilder.DropTable(
                name: "payment_items");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "space_availabilities");

            migrationBuilder.DropTable(
                name: "space_photos");

            migrationBuilder.DropTable(
                name: "space_rates");

            migrationBuilder.DropTable(
                name: "user_qr_historys");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "menu_item_options");

            migrationBuilder.DropTable(
                name: "member_fees");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "spaces");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "member_types_fees");

            migrationBuilder.DropTable(
                name: "accounting_entries");

            migrationBuilder.DropTable(
                name: "document_types");

            migrationBuilder.DropTable(
                name: "payment_types");

            migrationBuilder.DropTable(
                name: "space_types");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "user_types");

            migrationBuilder.DropTable(
                name: "fee_configurations");

            migrationBuilder.DropTable(
                name: "source_modules");

            migrationBuilder.DropTable(
                name: "member_statuses");

            migrationBuilder.DropTable(
                name: "member_types");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "ubigeos");

            migrationBuilder.DropTable(
                name: "accounting_charts");

            migrationBuilder.DropTable(
                name: "periodicities");

            migrationBuilder.DropTable(
                name: "account_types");
        }
    }
}
