using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserEntityId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "RefreshToken",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserEntityId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerify",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerifyToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "IsVerify",
                table: "User");

            migrationBuilder.DropColumn(
                name: "VerifyToken",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RefreshToken",
                newName: "UserEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserEntityId",
                table: "RefreshToken",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
