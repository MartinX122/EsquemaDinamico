using Microsoft.EntityFrameworkCore.Migrations;

namespace EsquemaDinamico.Data.Migrations
{
    public partial class Esquema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tablas",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tablas", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Campos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreTabla = table.Column<string>(type: "TEXT", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Desc = table.Column<string>(type: "TEXT", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Campos_Tablas_NombreTabla",
                        column: x => x.NombreTabla,
                        principalTable: "Tablas",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campos_NombreTabla",
                table: "Campos",
                column: "NombreTabla");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campos");

            migrationBuilder.DropTable(
                name: "Tablas");
        }
    }
}
