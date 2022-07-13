using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListAnimalEntity_ReportTicket_ReportTicketId",
                table: "ListAnimalEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_SealTabEntity_ReportTicket_ReportTicketId",
                table: "SealTabEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SealTabEntity",
                table: "SealTabEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListAnimalEntity",
                table: "ListAnimalEntity");

            migrationBuilder.RenameTable(
                name: "SealTabEntity",
                newName: "SealTab");

            migrationBuilder.RenameTable(
                name: "ListAnimalEntity",
                newName: "ListAnimal");

            migrationBuilder.RenameIndex(
                name: "IX_SealTabEntity_ReportTicketId",
                table: "SealTab",
                newName: "IX_SealTab_ReportTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_ListAnimalEntity_ReportTicketId",
                table: "ListAnimal",
                newName: "IX_ListAnimal_ReportTicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SealTab",
                table: "SealTab",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListAnimal",
                table: "ListAnimal",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SealConfig",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SealCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SealConfig", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ListAnimal_ReportTicket_ReportTicketId",
                table: "ListAnimal",
                column: "ReportTicketId",
                principalTable: "ReportTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SealTab_ReportTicket_ReportTicketId",
                table: "SealTab",
                column: "ReportTicketId",
                principalTable: "ReportTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListAnimal_ReportTicket_ReportTicketId",
                table: "ListAnimal");

            migrationBuilder.DropForeignKey(
                name: "FK_SealTab_ReportTicket_ReportTicketId",
                table: "SealTab");

            migrationBuilder.DropTable(
                name: "SealConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SealTab",
                table: "SealTab");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListAnimal",
                table: "ListAnimal");

            migrationBuilder.RenameTable(
                name: "SealTab",
                newName: "SealTabEntity");

            migrationBuilder.RenameTable(
                name: "ListAnimal",
                newName: "ListAnimalEntity");

            migrationBuilder.RenameIndex(
                name: "IX_SealTab_ReportTicketId",
                table: "SealTabEntity",
                newName: "IX_SealTabEntity_ReportTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_ListAnimal_ReportTicketId",
                table: "ListAnimalEntity",
                newName: "IX_ListAnimalEntity_ReportTicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SealTabEntity",
                table: "SealTabEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListAnimalEntity",
                table: "ListAnimalEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListAnimalEntity_ReportTicket_ReportTicketId",
                table: "ListAnimalEntity",
                column: "ReportTicketId",
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
    }
}
