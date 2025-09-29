using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payments_member_fees_member_fee_id",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "ix_payment_receiptnumber",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "ix_payments_member_fee_id",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "member_fee_id",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "payments",
                newName: "total_amount");

            migrationBuilder.RenameIndex(
                name: "ix_payments_accounting_entry_id",
                table: "payments",
                newName: "ix_payment_accountingentryid");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "payment_date",
                table: "payments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

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
                name: "ix_payment_receiptnumber",
                table: "payments",
                column: "receipt_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payment_state",
                table: "payments",
                column: "state_payment");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_items");

            migrationBuilder.DropIndex(
                name: "ix_payment_receiptnumber",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "ix_payment_state",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "payments",
                newName: "amount");

            migrationBuilder.RenameIndex(
                name: "ix_payment_accountingentryid",
                table: "payments",
                newName: "ix_payments_accounting_entry_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "payment_date",
                table: "payments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<long>(
                name: "member_fee_id",
                table: "payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_payment_receiptnumber",
                table: "payments",
                column: "receipt_number");

            migrationBuilder.CreateIndex(
                name: "ix_payments_member_fee_id",
                table: "payments",
                column: "member_fee_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payments_member_fees_member_fee_id",
                table: "payments",
                column: "member_fee_id",
                principalTable: "member_fees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
