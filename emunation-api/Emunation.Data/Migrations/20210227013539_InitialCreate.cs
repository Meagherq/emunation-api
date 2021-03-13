using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Emunation.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserGameProfiles",
                columns: table => new
                {
                    UserGameProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false),
                    IsRecent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameProfiles", x => x.UserGameProfileId);
                    table.ForeignKey(
                        name: "FK_UserGameProfiles_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGameProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSaves",
                columns: table => new
                {
                    UserSaveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGameProfileId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    LastPlayed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSaves", x => x.UserSaveId);
                    table.ForeignKey(
                        name: "FK_UserSaves_UserGameProfiles_UserGameProfileId",
                        column: x => x.UserGameProfileId,
                        principalTable: "UserGameProfiles",
                        principalColumn: "UserGameProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGameProfiles_GameId",
                table: "UserGameProfiles",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGameProfiles_UserId",
                table: "UserGameProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSaves_UserGameProfileId",
                table: "UserSaves",
                column: "UserGameProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSaves");

            migrationBuilder.DropTable(
                name: "UserGameProfiles");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
