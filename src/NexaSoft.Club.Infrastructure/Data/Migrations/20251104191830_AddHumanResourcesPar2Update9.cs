using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "contract_type_id",
                table: "employees_info",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "contract_types",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_employees_info_contract_type_id",
                table: "employees_info",
                column: "contract_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_info_contract_types_contract_type_id",
                table: "employees_info",
                column: "contract_type_id",
                principalTable: "contract_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_info_contract_types_contract_type_id",
                table: "employees_info");

            migrationBuilder.DropIndex(
                name: "ix_employees_info_contract_type_id",
                table: "employees_info");

            migrationBuilder.DropColumn(
                name: "contract_type_id",
                table: "employees_info");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "contract_types",
                type: "character varying(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
