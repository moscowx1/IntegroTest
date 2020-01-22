using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excels",
                columns: table => new
                {
                    ExcelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excels", x => x.ExcelId);
                });

            migrationBuilder.CreateTable(
                name: "TempExcels",
                columns: table => new
                {
                    TempExcelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempExcels", x => x.TempExcelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excels");

            migrationBuilder.DropTable(
                name: "TempExcels");
        }
    }
}
