using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class PontuacaoUserIdandForeignKeyNotUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "utilizadorUsername",
                table: "Pontuacao");

            migrationBuilder.AddColumn<string>(
                name: "utilizadorId",
                table: "Pontuacao",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pontuacao_utilizadorId",
                table: "Pontuacao",
                column: "utilizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pontuacao_AspNetUsers_utilizadorId",
                table: "Pontuacao",
                column: "utilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pontuacao_AspNetUsers_utilizadorId",
                table: "Pontuacao");

            migrationBuilder.DropIndex(
                name: "IX_Pontuacao_utilizadorId",
                table: "Pontuacao");

            migrationBuilder.DropColumn(
                name: "utilizadorId",
                table: "Pontuacao");

            migrationBuilder.AddColumn<string>(
                name: "utilizadorUsername",
                table: "Pontuacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
