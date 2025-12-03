using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMenuRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles");

            migrationBuilder.CreateIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles",
                columns: new[] { "menu_item_option_id", "role_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles");

            migrationBuilder.CreateIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles",
                column: "role_id");
        }
    }
}
