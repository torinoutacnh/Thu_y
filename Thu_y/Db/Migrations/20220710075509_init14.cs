using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id_Pricing",
                table: "SealTabEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "SealTabEntity",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayAge",
                table: "ListAnimalEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ListAnimalEntity",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Pricing",
                table: "Animal",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Pricing",
                table: "SealTabEntity");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SealTabEntity");

            migrationBuilder.DropColumn(
                name: "DayAge",
                table: "ListAnimalEntity");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ListAnimalEntity");

            migrationBuilder.DropColumn(
                name: "Pricing",
                table: "Animal");
        }
    }
}
