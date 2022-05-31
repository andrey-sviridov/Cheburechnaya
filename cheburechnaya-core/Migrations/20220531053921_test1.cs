using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cheburechnaya_core.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Users_UsersId",
                table: "PostUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser");

            migrationBuilder.DropIndex(
                name: "IX_PostUser_UsersId",
                table: "PostUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "PostUser",
                newName: "LikedUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser",
                columns: new[] { "LikedUsersId", "PostsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_PostsId",
                table: "PostUser",
                column: "PostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Users_LikedUsersId",
                table: "PostUser",
                column: "LikedUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Users_LikedUsersId",
                table: "PostUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser");

            migrationBuilder.DropIndex(
                name: "IX_PostUser_PostsId",
                table: "PostUser");

            migrationBuilder.RenameColumn(
                name: "LikedUsersId",
                table: "PostUser",
                newName: "UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostUser",
                table: "PostUser",
                columns: new[] { "PostsId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_UsersId",
                table: "PostUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Users_UsersId",
                table: "PostUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
