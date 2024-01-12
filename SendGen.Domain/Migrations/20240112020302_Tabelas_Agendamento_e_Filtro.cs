using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendGen.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Tabelas_Agendamento_e_Filtro : Migration
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
                    FiltroID = table.Column<int>(type: "int", nullable: false),
                    TemplateID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaExecucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntervaloExecucao = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FiltroDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condicao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltroDB", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "FiltroDB");
        }
    }
}
