using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendGen.Domain.Migrations;

/// <inheritdoc />
public partial class AddTabela_FiltroDB : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
	name: "FiltroDB");
	}
}
