using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cheburechnaya_core.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostUser");

            migrationBuilder.CreateTable(
                name: "LikeInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeInformations_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeInformations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeInformations_PostId",
                table: "LikeInformations",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeInformations_UserId",
                table: "LikeInformations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeInformations");

            migrationBuilder.CreateTable(
                name: "PostUser",
                columns: table => new
                {
                    PostLikedId = table.Column<int>(type: "int", nullable: false),
                    UserLikedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUser", x => new { x.PostLikedId, x.UserLikedId });
                    table.ForeignKey(
                        name: "FK_PostUser_Posts_PostLikedId",
                        column: x => x.PostLikedId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUser_Users_UserLikedId",
                        column: x => x.UserLikedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_UserLikedId",
                table: "PostUser",
                column: "UserLikedId");
        }
    }
}
