using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogAPI.Migrations
{
    /// <inheritdoc />
    public partial class communityPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentsPosts",
                columns: table => new
                {
                    postId = table.Column<Guid>(type: "uuid", nullable: false),
                    commentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsPosts", x => new { x.postId, x.commentId });
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    isClosed = table.Column<bool>(type: "boolean", nullable: false),
                    subscribersCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Like = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.UserId, x.PostId });
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    readingTime = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    authorId = table.Column<Guid>(type: "uuid", nullable: false),
                    authorName = table.Column<string>(type: "text", nullable: false),
                    communityId = table.Column<Guid>(type: "uuid", nullable: false),
                    communityName = table.Column<string>(type: "text", nullable: false),
                    addressId = table.Column<Guid>(type: "uuid", nullable: false),
                    likes = table.Column<int>(type: "integer", nullable: false),
                    hasLikes = table.Column<bool>(type: "boolean", nullable: false),
                    commentsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    postId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.postId, x.tagId });
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullName = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    createTime = table.Column<DateOnly>(type: "date", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    CommunityModelid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Communities_CommunityModelid",
                        column: x => x.CommunityModelid,
                        principalTable: "Communities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    createDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    authorId = table.Column<Guid>(type: "uuid", nullable: false),
                    subComments = table.Column<int>(type: "integer", nullable: false),
                    parentCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    postId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostModelid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostModelid",
                        column: x => x.PostModelid,
                        principalTable: "Posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createTime = table.Column<DateOnly>(type: "date", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    PostModelid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tags_Posts_PostModelid",
                        column: x => x.PostModelid,
                        principalTable: "Posts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Id",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostModelid",
                table: "Comments",
                column: "PostModelid");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_id",
                table: "Communities",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_id",
                table: "Posts",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_id",
                table: "Tags",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostModelid",
                table: "Tags",
                column: "PostModelid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommunityModelid",
                table: "Users",
                column: "CommunityModelid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_fullName",
                table: "Users",
                column: "fullName");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_Id",
                table: "UserTokens",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CommentsPosts");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Communities");
        }
    }
}
