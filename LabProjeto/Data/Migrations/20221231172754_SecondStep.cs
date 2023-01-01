using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class SecondStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JogoModel_categoriaId",
                table: "JogoModel",
                column: "categoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_JogoModel_CategoriaModel_categoriaId",
                table: "JogoModel",
                column: "categoriaId",
                principalTable: "CategoriaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JogoModel_CategoriaModel_categoriaId",
                table: "JogoModel");

            migrationBuilder.DropIndex(
                name: "IX_JogoModel_categoriaId",
                table: "JogoModel");
        }
    }
}
