using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainPage",
                table: "ReceiptAllocate",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainPage",
                table: "ReceiptAllocate");
        }
    }
}
