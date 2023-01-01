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
                name: "JogoCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jogoId = table.Column<int>(type: "int", nullable: false),
                    categoriaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoCategoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    perfilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    utilizadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cargoId = table.Column<int>(type: "int", nullable: false),
                    saldo = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilModel_AspNetUsers_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilModel_CargoModel_cargoId",
                        column: x => x.cargoId,
                        principalTable: "CargoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perfilId = table.Column<int>(type: "int", nullable: false),
                    categoriaID = table.Column<int>(type: "int", nullable: false),
                    PerfilModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilCategoria_PerfilModel_PerfilModelId",
                        column: x => x.PerfilModelId,
                        principalTable: "PerfilModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerfilJogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perfilId = table.Column<int>(type: "int", nullable: false),
                    jogoId = table.Column<int>(type: "int", nullable: false),
                    PerfilModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilJogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilJogos_PerfilModel_PerfilModelId",
                        column: x => x.PerfilModelId,
                        principalTable: "PerfilModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilCategoria_PerfilModelId",
                table: "PerfilCategoria",
                column: "PerfilModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilJogos_PerfilModelId",
                table: "PerfilJogos",
                column: "PerfilModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilModel_cargoId",
                table: "PerfilModel",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilModel_utilizadorId",
                table: "PerfilModel",
                column: "utilizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaModel");

            migrationBuilder.DropTable(
                name: "JogoCategoria");

            migrationBuilder.DropTable(
                name: "JogoModel");

            migrationBuilder.DropTable(
                name: "PerfilCargo");

            migrationBuilder.DropTable(
                name: "PerfilCategoria");

            migrationBuilder.DropTable(
                name: "PerfilJogos");

            migrationBuilder.DropTable(
                name: "PerfilModel");

            migrationBuilder.DropTable(
                name: "CargoModel");
        }
    }
}
