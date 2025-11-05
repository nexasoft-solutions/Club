using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "employee_info_id",
                table: "employment_contracts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "employment_contracts",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_employment_contracts_employee_info_id",
                table: "employment_contracts",
                column: "employee_info_id");

            migrationBuilder.AddForeignKey(
                name: "fk_employment_contracts_employees_info_employee_info_id",
                table: "employment_contracts",
                column: "employee_info_id",
                principalTable: "employees_info",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employment_contracts_employees_info_employee_info_id",
                table: "employment_contracts");

            migrationBuilder.DropIndex(
                name: "ix_employment_contracts_employee_info_id",
                table: "employment_contracts");

            migrationBuilder.DropColumn(
                name: "employee_info_id",
                table: "employment_contracts");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "employment_contracts");

            migrationBuilder.AddColumn<long>(
                name: "contract_type_id",
                table: "employees_info",
                type: "bigint",
                nullable: true);

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
    }
}
