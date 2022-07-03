using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thu_y.Db.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserScheduleEntity_Abattoir_AbattoirEntityId",
                table: "UserScheduleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScheduleEntity_UserEntity_UserId",
                table: "UserScheduleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserScheduleEntity",
                table: "UserScheduleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity");

            migrationBuilder.RenameTable(
                name: "UserScheduleEntity",
                newName: "UserSchedule");

            migrationBuilder.RenameTable(
                name: "UserEntity",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_UserScheduleEntity_UserId",
                table: "UserSchedule",
                newName: "IX_UserSchedule_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserScheduleEntity_AbattoirEntityId",
                table: "UserSchedule",
                newName: "IX_UserSchedule_AbattoirEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedule",
                table: "UserSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Abattoir_AbattoirEntityId",
                table: "UserSchedule",
                column: "AbattoirEntityId",
                principalTable: "Abattoir",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_User_UserId",
                table: "UserSchedule",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Abattoir_AbattoirEntityId",
                table: "UserSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_User_UserId",
                table: "UserSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedule",
                table: "UserSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "UserSchedule",
                newName: "UserScheduleEntity");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_UserId",
                table: "UserScheduleEntity",
                newName: "IX_UserScheduleEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_AbattoirEntityId",
                table: "UserScheduleEntity",
                newName: "IX_UserScheduleEntity_AbattoirEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserScheduleEntity",
                table: "UserScheduleEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntity",
                table: "UserEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScheduleEntity_Abattoir_AbattoirEntityId",
                table: "UserScheduleEntity",
                column: "AbattoirEntityId",
                principalTable: "Abattoir",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScheduleEntity_UserEntity_UserId",
                table: "UserScheduleEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
