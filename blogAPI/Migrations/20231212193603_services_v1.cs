using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogAPI.Migrations
{
    /// <inheritdoc />
    public partial class servicesv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Communities_CommunityModelid",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommunityModelid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommunityModelid",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    InvalidToken = table.Column<string>(type: "text", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.InvalidToken);
                });

            migrationBuilder.CreateTable(
                name: "UserDto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullName = table.Column<string>(type: "text", nullable: false),
                    createTime = table.Column<DateOnly>(type: "date", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    CommunityModelid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDto", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserDto_Communities_CommunityModelid",
                        column: x => x.CommunityModelid,
                        principalTable: "Communities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDto_CommunityModelid",
                table: "UserDto",
                column: "CommunityModelid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "UserDto");

            migrationBuilder.AddColumn<Guid>(
                name: "CommunityModelid",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommunityModelid",
                table: "Users",
                column: "CommunityModelid");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_Id",
                table: "UserTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Communities_CommunityModelid",
                table: "Users",
                column: "CommunityModelid",
                principalTable: "Communities",
                principalColumn: "id");
        }
    }
}
