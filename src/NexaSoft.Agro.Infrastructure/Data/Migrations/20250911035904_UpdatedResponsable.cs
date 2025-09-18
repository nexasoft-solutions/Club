using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Agro.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedResponsable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "estudio_ambiental_id",
                table: "responsables",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_responsables_estudio_ambiental_id",
                table: "responsables",
                column: "estudio_ambiental_id");

            migrationBuilder.AddForeignKey(
                name: "fk_responsables_estudios_ambientales_estudio_ambiental_id",
                table: "responsables",
                column: "estudio_ambiental_id",
                principalTable: "estudios_ambientales",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_responsables_estudios_ambientales_estudio_ambiental_id",
                table: "responsables");

            migrationBuilder.DropIndex(
                name: "ix_responsables_estudio_ambiental_id",
                table: "responsables");

            migrationBuilder.DropColumn(
                name: "estudio_ambiental_id",
                table: "responsables");
        }
    }
}
