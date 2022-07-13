using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListAnimalEntity_ReportTicket_ReportTicketId",
                table: "ListAnimalEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptAllocate_Receipt_ReceiptId",
                table: "ReceiptAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportTicketValue_ReportTicket_ReportId",
                table: "ReportTicketValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SealTabEntity_ReportTicket_ReportTicketId",
                table: "SealTabEntity");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "ReportTicket");

            migrationBuilder.AlterColumn<string>(
                name: "ReportTicketId",
                table: "SealTabEntity",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReportId",
                table: "ReportTicketValue",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptId",
                table: "ReceiptAllocate",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReportTicketId",
                table: "ListAnimalEntity",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ListAnimalEntity_ReportTicket_ReportTicketId",
                table: "ListAnimalEntity",
                column: "ReportTicketId",
                principalTable: "ReportTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptAllocate_Receipt_ReceiptId",
                table: "ReceiptAllocate",
                column: "ReceiptId",
                principalTable: "Receipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTicketValue_ReportTicket_ReportId",
                table: "ReportTicketValue",
                column: "ReportId",
                principalTable: "ReportTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SealTabEntity_ReportTicket_ReportTicketId",
                table: "SealTabEntity",
                column: "ReportTicketId",
                principalTable: "ReportTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListAnimalEntity_ReportTicket_ReportTicketId",
                table: "ListAnimalEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptAllocate_Receipt_ReceiptId",
                table: "ReceiptAllocate");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportTicketValue_ReportTicket_ReportId",
                table: "ReportTicketValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SealTabEntity_ReportTicket_ReportTicketId",
                table: "SealTabEntity");

            migrationBuilder.AlterColumn<string>(
                name: "ReportTicketId",
                table: "SealTabEntity",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ReportId",
                table: "ReportTicketValue",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AttributeId",
                table: "ReportTicket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptId",
                table: "ReceiptAllocate",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ReportTicketId",
                table: "ListAnimalEntity",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ListAnimalEntity_ReportTicket_ReportTicketId",
                table: "ListAnimalEntity",
                column: "ReportTicketId",
                principalTable: "ReportTicket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptAllocate_Receipt_ReceiptId",
                table: "ReceiptAllocate",
                column: "ReceiptId",
                principalTable: "Receipt",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTicketValue_ReportTicket_ReportId",
                table: "ReportTicketValue",
                column: "ReportId",
                principalTable: "ReportTicket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SealTabEntity_ReportTicket_ReportTicketId",
                table: "SealTabEntity",
                column: "ReportTicketId",
                principalTable: "ReportTicket",
                principalColumn: "Id");
        }
    }
}
