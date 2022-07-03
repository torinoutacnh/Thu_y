using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAttribute_ReportTicketValue_ValueId",
                table: "FormAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Abattoir_AbattoirEntityId",
                table: "UserSchedule");

            migrationBuilder.DropIndex(
                name: "IX_UserSchedule_AbattoirEntityId",
                table: "UserSchedule");

            migrationBuilder.DropIndex(
                name: "IX_FormAttribute_ValueId",
                table: "FormAttribute");

            migrationBuilder.DropColumn(
                name: "AbattoirEntityId",
                table: "UserSchedule");

            migrationBuilder.DropColumn(
                name: "ValueId",
                table: "FormAttribute");

            migrationBuilder.AddColumn<string>(
                name: "AttributeId",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "ReportTicketValue");

            migrationBuilder.AddColumn<string>(
                name: "AbattoirEntityId",
                table: "UserSchedule",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueId",
                table: "FormAttribute",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_AbattoirEntityId",
                table: "UserSchedule",
                column: "AbattoirEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAttribute_ValueId",
                table: "FormAttribute",
                column: "ValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAttribute_ReportTicketValue_ValueId",
                table: "FormAttribute",
                column: "ValueId",
                principalTable: "ReportTicketValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Abattoir_AbattoirEntityId",
                table: "UserSchedule",
                column: "AbattoirEntityId",
                principalTable: "Abattoir",
                principalColumn: "Id");
        }
    }
}
