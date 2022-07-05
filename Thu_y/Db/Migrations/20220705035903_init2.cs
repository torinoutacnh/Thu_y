using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbattoirDetail_Abattoir_AbattoirId",
                table: "AbattoirDetail");

            migrationBuilder.DropIndex(
                name: "IX_AbattoirDetail_AbattoirId",
                table: "AbattoirDetail");

            migrationBuilder.DropColumn(
                name: "AbattoirId",
                table: "AbattoirDetail");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Vacine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AbattoirAddress",
                table: "UserSchedule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AbattoirName",
                table: "UserSchedule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AttributeControlType",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributeDataType",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormCode",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormName",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormNumber",
                table: "ReportTicketValue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ReportTicketValue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ReportTicket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FormAttribute",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Form",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Abattoir",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Page = table.Column<int>(type: "int", nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptReport",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptAllocateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUse = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PageUse = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptReport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptAllocate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TotalPage = table.Column<int>(type: "int", nullable: false),
                    ReceiptName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptAllocate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptAllocate_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptAllocate_ReceiptId",
                table: "ReceiptAllocate",
                column: "ReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptAllocate");

            migrationBuilder.DropTable(
                name: "ReceiptReport");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vacine");

            migrationBuilder.DropColumn(
                name: "AbattoirAddress",
                table: "UserSchedule");

            migrationBuilder.DropColumn(
                name: "AbattoirName",
                table: "UserSchedule");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserSchedule");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AttributeControlType",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "AttributeDataType",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "AttributeName",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "FormCode",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "FormName",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "FormNumber",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReportTicketValue");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReportTicket");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FormAttribute");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Abattoir");

            migrationBuilder.AddColumn<string>(
                name: "AbattoirId",
                table: "AbattoirDetail",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AbattoirDetail_AbattoirId",
                table: "AbattoirDetail",
                column: "AbattoirId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbattoirDetail_Abattoir_AbattoirId",
                table: "AbattoirDetail",
                column: "AbattoirId",
                principalTable: "Abattoir",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
