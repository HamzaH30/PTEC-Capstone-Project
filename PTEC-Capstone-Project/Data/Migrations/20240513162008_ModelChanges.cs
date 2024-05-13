using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTEC_Capstone_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "GroupUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorID",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GameID",
                table: "Posts",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GroupID",
                table: "Posts",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_StatusID",
                table: "Posts",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserID",
                table: "GroupUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorID",
                table: "Groups",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GameID",
                table: "Groups",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_StatusID",
                table: "Groups",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_GameTournaments_GameID",
                table: "GameTournaments",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameTournaments_TournamentID",
                table: "GameTournaments",
                column: "TournamentID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTournaments_Games_GameID",
                table: "GameTournaments",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTournaments_Tournaments_TournamentID",
                table: "GameTournaments",
                column: "TournamentID",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_CreatorID",
                table: "Groups",
                column: "CreatorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Games_GameID",
                table: "Groups",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupStatuses_StatusID",
                table: "Groups",
                column: "StatusID",
                principalTable: "GroupStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_AspNetUsers_UserID",
                table: "GroupUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Games_GameID",
                table: "Posts",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Groups_GroupID",
                table: "Posts",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostStatuses_StatusID",
                table: "Posts",
                column: "StatusID",
                principalTable: "PostStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTournaments_Games_GameID",
                table: "GameTournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTournaments_Tournaments_TournamentID",
                table: "GameTournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_CreatorID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Games_GameID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupStatuses_StatusID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_AspNetUsers_UserID",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Games_GameID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Groups_GroupID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostStatuses_StatusID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_GameID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_GroupID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_StatusID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_UserID",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatorID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GameID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_StatusID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GameTournaments_GameID",
                table: "GameTournaments");

            migrationBuilder.DropIndex(
                name: "IX_GameTournaments_TournamentID",
                table: "GameTournaments");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AspNetUsers",
                newName: "Username");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "GroupUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorID",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
