using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendGen.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddColuna_Cliente_TraCod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TraCod",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TraCod",
                table: "Cliente");
        }
    }
}
