using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApproveId",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApproveName",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributeId",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "ApproveId",
                table: "ReportTicket");

            migrationBuilder.DropColumn(
                name: "ApproveName",
                table: "ReportTicket");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "ReportTicket");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "ReportTicket");
        }
    }
}
