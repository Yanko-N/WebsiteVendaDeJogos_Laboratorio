using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class eightStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JogoCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jogoId = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JogoCategoria_CategoriaModel_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "CategoriaModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JogoCategoria_JogoModel_jogoId",
                        column: x => x.jogoId,
                        principalTable: "JogoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogoCategoria_categoriaId",
                table: "JogoCategoria",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_JogoCategoria_jogoId",
                table: "JogoCategoria",
                column: "jogoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogoCategoria");
        }
    }
}
