using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class SixStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilJogos_PerfilModel_PerfilModelId",
                table: "PerfilJogos");

            migrationBuilder.DropIndex(
                name: "IX_PerfilJogos_PerfilModelId",
                table: "PerfilJogos");

            migrationBuilder.DropColumn(
                name: "PerfilModelId",
                table: "PerfilJogos");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilJogos_jogoId",
                table: "PerfilJogos",
                column: "jogoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilJogos_perfilId",
                table: "PerfilJogos",
                column: "perfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilJogos_JogoModel_jogoId",
                table: "PerfilJogos",
                column: "jogoId",
                principalTable: "JogoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilJogos_PerfilModel_perfilId",
                table: "PerfilJogos",
                column: "perfilId",
                principalTable: "PerfilModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilJogos_JogoModel_jogoId",
                table: "PerfilJogos");

            migrationBuilder.DropForeignKey(
                name: "FK_PerfilJogos_PerfilModel_perfilId",
                table: "PerfilJogos");

            migrationBuilder.DropIndex(
                name: "IX_PerfilJogos_jogoId",
                table: "PerfilJogos");

            migrationBuilder.DropIndex(
                name: "IX_PerfilJogos_perfilId",
                table: "PerfilJogos");

            migrationBuilder.AddColumn<int>(
                name: "PerfilModelId",
                table: "PerfilJogos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilJogos_PerfilModelId",
                table: "PerfilJogos",
                column: "PerfilModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilJogos_PerfilModel_PerfilModelId",
                table: "PerfilJogos",
                column: "PerfilModelId",
                principalTable: "PerfilModel",
                principalColumn: "Id");
        }
    }
}
