using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTEC_Capstone_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class changingPropNames : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Requests_SenderID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RecieverID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RecieverID",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "RecieverID",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SenderID",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RecieverID",
                table: "Requests",
                column: "RecieverID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderID",
                table: "Notifications",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_SenderID",
                table: "Notifications",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RecieverID",
                table: "Requests",
                column: "RecieverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_SenderID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RecieverID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RecieverID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SenderID",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "RecieverID",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SenderID",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderID",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RecieverID",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SenderID",
                table: "Requests",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecieverID",
                table: "Notifications",
                column: "RecieverID");

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
    }
}
