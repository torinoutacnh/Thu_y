using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "ReportTicketValue");

            migrationBuilder.AddColumn<string>(
                name: "Col_Design",
                table: "FormAttribute",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "api_DropDownlist",
                table: "FormAttribute",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Col_Design",
                table: "FormAttribute");

            migrationBuilder.DropColumn(
                name: "api_DropDownlist",
                table: "FormAttribute");

            migrationBuilder.AddColumn<string>(
                name: "AnimalId",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
