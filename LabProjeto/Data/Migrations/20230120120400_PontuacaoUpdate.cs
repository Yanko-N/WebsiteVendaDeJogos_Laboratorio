using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class PontuacaoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "utilizadorUsername",
                table: "Pontuacao",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "utilizadorUsername",
                table: "Pontuacao");
        }
    }
}
