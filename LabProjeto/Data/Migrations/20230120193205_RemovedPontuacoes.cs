using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class RemovedPontuacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontuacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pontuacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogoId = table.Column<int>(type: "int", nullable: true),
                    utilizadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pontuacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontuacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontuacao_AspNetUsers_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pontuacao_JogoModel_JogoId",
                        column: x => x.JogoId,
                        principalTable: "JogoModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pontuacao_JogoId",
                table: "Pontuacao",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontuacao_utilizadorId",
                table: "Pontuacao",
                column: "utilizadorId");
        }
    }
}
