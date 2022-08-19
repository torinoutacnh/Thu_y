using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Migrations
{
    public partial class update0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttributeGroup",
                table: "FormAttribute",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeGroup",
                table: "FormAttribute");
        }
    }
}
