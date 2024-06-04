using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTEC_Capstone_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatingNotifsAndReqs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RecieverID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "PostID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_PostID",
                table: "Requests",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Posts_PostID",
                table: "Requests",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Posts_PostID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_PostID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "RecieverID",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderID",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
    }
}
