using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoteBook",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteBook",
                table: "ReportTicket");
        }
    }
}
