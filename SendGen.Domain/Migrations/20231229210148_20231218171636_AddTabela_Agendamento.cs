using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendGen.Domain.Migrations
{
    /// <inheritdoc />
    public partial class _20231218171636_AddTabela_Agendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiltroID = table.Column<string>(type: "int", nullable: false),
                    TemplateID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaExecucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntervaloExecucao = table.Column<int>(type: "int", nullable: false),
                    Condicao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");
        }
    }
}
