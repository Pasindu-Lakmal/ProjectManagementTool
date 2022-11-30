using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTaskeditaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AppUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Assignee",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Tasks",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AppUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AssigneeId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Tasks",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                newName: "IX_Tasks_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "Assignee",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AppUserId",
                table: "Tasks",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
