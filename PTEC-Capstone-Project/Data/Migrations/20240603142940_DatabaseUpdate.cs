using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTEC_Capstone_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_RecieverID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRequests_Posts_PostID",
                table: "UserRequests");

            migrationBuilder.DropIndex(
                name: "IX_UserRequests_PostID",
                table: "UserRequests");

            migrationBuilder.AlterColumn<string>(
                name: "SenderID",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RecieverID",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RecieverID",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SenderID",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_RecieverID",
                table: "Notifications",
                column: "RecieverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_SenderID",
                table: "Requests",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_RecieverID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RecieverID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "SenderID",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecieverID",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_PostID",
                table: "UserRequests",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_RecieverID",
                table: "Notifications",
                column: "RecieverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_SenderID",
                table: "Requests",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequests_Posts_PostID",
                table: "UserRequests",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
