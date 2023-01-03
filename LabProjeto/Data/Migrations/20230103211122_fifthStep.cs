using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class fifthStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerfilCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perfilId = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilCategoria", x => x.Id);
                   
                    table.ForeignKey(
                        name: "FK_PerfilCategoria_CategoriaModel_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "CategoriaModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                   
                    table.ForeignKey(
                        name: "FK_PerfilCategoria_PerfilModel_perfilId",
                        column: x => x.perfilId,
                        principalTable: "PerfilModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilCategoria_categoriaId",
                table: "PerfilCategoria",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilCategoria_perfilId",
                table: "PerfilCategoria",
                column: "perfilId");
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilCategoria");
        }
    }
}
