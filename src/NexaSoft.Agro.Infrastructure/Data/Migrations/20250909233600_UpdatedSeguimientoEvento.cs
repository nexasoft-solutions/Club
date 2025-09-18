using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Agro.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeguimientoEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cumplimientos_seguimientos_cumplimientos_cumplimiento_id",
                table: "cumplimientos_seguimientos");

            migrationBuilder.RenameColumn(
                name: "cumplimiento_id",
                table: "cumplimientos_seguimientos",
                newName: "evento_regulatorio_id");

            migrationBuilder.RenameIndex(
                name: "ix_cumplimientos_seguimientos_cumplimiento_id",
                table: "cumplimientos_seguimientos",
                newName: "ix_cumplimientos_seguimientos_evento_regulatorio_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cumplimientos_seguimientos_evento_regulatorio_evento_regula",
                table: "cumplimientos_seguimientos",
                column: "evento_regulatorio_id",
                principalTable: "eventos_regulatorios",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cumplimientos_seguimientos_evento_regulatorio_evento_regula",
                table: "cumplimientos_seguimientos");

            migrationBuilder.RenameColumn(
                name: "evento_regulatorio_id",
                table: "cumplimientos_seguimientos",
                newName: "cumplimiento_id");

            migrationBuilder.RenameIndex(
                name: "ix_cumplimientos_seguimientos_evento_regulatorio_id",
                table: "cumplimientos_seguimientos",
                newName: "ix_cumplimientos_seguimientos_cumplimiento_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cumplimientos_seguimientos_cumplimientos_cumplimiento_id",
                table: "cumplimientos_seguimientos",
                column: "cumplimiento_id",
                principalTable: "cumplimientos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
