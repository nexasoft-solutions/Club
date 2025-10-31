using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanResourcesPar2Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "payroll_type_id",
                table: "payroll_periods",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_payrollperiod_payrolltypeid",
                table: "payroll_periods",
                column: "payroll_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payroll_periods_payroll_type_payroll_type_id",
                table: "payroll_periods",
                column: "payroll_type_id",
                principalTable: "payroll_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payroll_periods_payroll_type_payroll_type_id",
                table: "payroll_periods");

            migrationBuilder.DropIndex(
                name: "ix_payrollperiod_payrolltypeid",
                table: "payroll_periods");

            migrationBuilder.DropColumn(
                name: "payroll_type_id",
                table: "payroll_periods");
        }
    }
}
