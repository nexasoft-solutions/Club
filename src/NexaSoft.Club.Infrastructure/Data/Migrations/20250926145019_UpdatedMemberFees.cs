using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMemberFees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_fees_fee_configurations_fee_configuration_id",
                table: "member_fees");

            migrationBuilder.RenameColumn(
                name: "fee_configuration_id",
                table: "member_fees",
                newName: "member_type_fee_id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_fees_member_type_fee_member_type_fee_id",
                table: "member_fees",
                column: "member_type_fee_id",
                principalTable: "member_types_fees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_member_fees_member_type_fee_member_type_fee_id",
                table: "member_fees");

            migrationBuilder.RenameColumn(
                name: "member_type_fee_id",
                table: "member_fees",
                newName: "fee_configuration_id");

            migrationBuilder.AddForeignKey(
                name: "fk_member_fees_fee_configurations_fee_configuration_id",
                table: "member_fees",
                column: "fee_configuration_id",
                principalTable: "fee_configurations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
