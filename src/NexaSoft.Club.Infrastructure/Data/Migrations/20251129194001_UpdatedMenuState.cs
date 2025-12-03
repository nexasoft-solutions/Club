using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Club.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMenuState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estado_menu",
                table: "menu_item_options",
                newName: "state_menu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state_menu",
                table: "menu_item_options",
                newName: "estado_menu");
        }
    }
}
