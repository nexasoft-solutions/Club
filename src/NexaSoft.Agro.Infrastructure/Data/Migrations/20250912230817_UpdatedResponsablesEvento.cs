using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexaSoft.Agro.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedResponsablesEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventos_regulatorios_responsables",
                columns: table => new
                {
                    evento_regulatorio_id = table.Column<long>(type: "bigint", nullable: false),
                    responsable_id = table.Column<long>(type: "bigint", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usuario_creacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_eventos_regulatorios_responsables", x => new { x.evento_regulatorio_id, x.responsable_id });
                    table.ForeignKey(
                        name: "fk_eventos_regulatorios_responsables_eventos_regulatorios_even",
                        column: x => x.evento_regulatorio_id,
                        principalTable: "eventos_regulatorios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventos_regulatorios_responsables");
        }
    }
}
