using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTEC_Capstone_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRequests_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRequests_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRequests_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_PostID",
                table: "UserRequests",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_RequestID",
                table: "UserRequests",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_UserID",
                table: "UserRequests",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRequests");
        }
    }
}
