using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportTicket_Form_FormId",
                table: "ReportTicket");

            migrationBuilder.DropIndex(
                name: "IX_ReportTicket_FormId",
                table: "ReportTicket");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "ReportTicket");

            migrationBuilder.AddColumn<string>(
                name: "AnimalId",
                table: "Vacine",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AttributeId",
                table: "ReportTicketValue",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vacine_AnimalId",
                table: "Vacine",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTicketValue_AttributeId",
                table: "ReportTicketValue",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTicketValue_FormAttribute_AttributeId",
                table: "ReportTicketValue",
                column: "AttributeId",
                principalTable: "FormAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacine_Animal_AnimalId",
                table: "Vacine",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportTicketValue_FormAttribute_AttributeId",
                table: "ReportTicketValue");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacine_Animal_AnimalId",
                table: "Vacine");

            migrationBuilder.DropIndex(
                name: "IX_Vacine_AnimalId",
                table: "Vacine");

            migrationBuilder.DropIndex(
                name: "IX_ReportTicketValue_AttributeId",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Vacine");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ReportTicket");

            migrationBuilder.AlterColumn<string>(
                name: "AttributeId",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FormId",
                table: "ReportTicket",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTicket_FormId",
                table: "ReportTicket",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTicket_Form_FormId",
                table: "ReportTicket",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
