using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class FirstStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissoesModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissoesModel", x => x.Id);
                });

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogoCategoria_JogoModel_jogoId",
                        column: x => x.jogoId,
                        principalTable: "JogoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoPermissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permissaoId = table.Column<int>(type: "int", nullable: false),
                    cargoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoPermissoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoPermissoes_CargoModel_cargoId",
                        column: x => x.cargoId,
                        principalTable: "CargoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoPermissoes_PermissoesModel_permissaoId",
                        column: x => x.permissaoId,
                        principalTable: "PermissoesModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoPermissoes_cargoId",
                table: "CargoPermissoes",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoPermissoes_permissaoId",
                table: "CargoPermissoes",
                column: "permissaoId");

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
                name: "CargoPermissoes");

            migrationBuilder.DropTable(
                name: "JogoCategoria");

            migrationBuilder.DropTable(
                name: "CargoModel");

            migrationBuilder.DropTable(
                name: "PermissoesModel");

            migrationBuilder.DropTable(
                name: "CategoriaModel");

            migrationBuilder.DropTable(
                name: "JogoModel");
        }
    }
}
