using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortNo",
                table: "FormAttribute",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortNo",
                table: "FormAttribute");
        }
    }
}
