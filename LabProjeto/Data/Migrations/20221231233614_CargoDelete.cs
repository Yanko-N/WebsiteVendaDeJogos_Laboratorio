using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabProjeto.Data.Migrations
{
    public partial class CargoDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilModel_CargoModel_cargoId",
                table: "PerfilModel");

            migrationBuilder.DropTable(
                name: "CargoModel");

            migrationBuilder.DropTable(
                name: "PerfilCargo");

            migrationBuilder.DropIndex(
                name: "IX_PerfilModel_cargoId",
                table: "PerfilModel");

            migrationBuilder.DropColumn(
                name: "cargoId",
                table: "PerfilModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cargoId",
                table: "PerfilModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_PerfilModel_cargoId",
                table: "PerfilModel",
                column: "cargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilModel_CargoModel_cargoId",
                table: "PerfilModel",
                column: "cargoId",
                principalTable: "CargoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
