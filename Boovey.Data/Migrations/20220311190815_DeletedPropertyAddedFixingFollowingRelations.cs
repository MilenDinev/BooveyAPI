using Microsoft.EntityFrameworkCore.Migrations;

namespace Boovey.Data.Migrations
{
    public partial class DeletedPropertyAddedFixingFollowingRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersFollowers");

            migrationBuilder.DropTable(
                name: "UsersFollowing");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Shelves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RequestType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RequestStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Quotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Publishers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    FollowedId = table.Column<int>(type: "int", nullable: false),
                    FollowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => new { x.FollowedId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_Follows_AspNetUsers_FollowedId",
                        column: x => x.FollowedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RequestType");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RequestStatus");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UsersFollowers",
                columns: table => new
                {
                    FollowerId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFollowers", x => new { x.FollowerId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersFollowers_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersFollowers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersFollowing",
                columns: table => new
                {
                    FollowingId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFollowing", x => new { x.FollowingId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersFollowing_AspNetUsers_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersFollowing_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFollowers_UserId",
                table: "UsersFollowers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFollowing_UserId",
                table: "UsersFollowing",
                column: "UserId");
        }
    }
}
