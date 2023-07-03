using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioCabs.Migrations
{
    /// <inheritdoc />
    public partial class addCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CarsImage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CarsModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CarsNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarsId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
