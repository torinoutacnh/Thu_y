using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerify",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "VerifyToken",
                table: "User",
                newName: "VerificationToken");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PasswordReset",
                table: "User",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Verified",
                table: "User",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "ReportTicketValue",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordReset",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "ReportTicketValue");

            migrationBuilder.RenameColumn(
                name: "VerificationToken",
                table: "User",
                newName: "VerifyToken");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerify",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
