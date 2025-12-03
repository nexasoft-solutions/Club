using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMenuRoles1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ix_rolepermissions_permissionid",
                table: "role_permissions",
                newName: "ix_role_permissions_permission_id");

            migrationBuilder.RenameIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles",
                newName: "ix_menu_roles_menu_role");

            migrationBuilder.CreateIndex(
                name: "ix_rolepermissions_role_permission",
                table: "role_permissions",
                columns: new[] { "role_id", "permission_id" });

            migrationBuilder.CreateIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_roles_role_role_id",
                table: "menu_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_roles_role_role_id",
                table: "menu_roles");

            migrationBuilder.DropIndex(
                name: "ix_rolepermissions_role_permission",
                table: "role_permissions");

            migrationBuilder.DropIndex(
                name: "ix_menu_roles_role_id",
                table: "menu_roles");

            migrationBuilder.RenameIndex(
                name: "ix_role_permissions_permission_id",
                table: "role_permissions",
                newName: "ix_rolepermissions_permissionid");

            migrationBuilder.RenameIndex(
                name: "ix_menu_roles_menu_role",
                table: "menu_roles",
                newName: "ix_menu_roles_role_id");
        }
    }
}
