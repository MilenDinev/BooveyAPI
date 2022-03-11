using Microsoft.EntityFrameworkCore.Migrations;

namespace Boovey.Data.Migrations
{
    public partial class CountryAndGenresAddedInAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AuthorId",
                table: "Genres",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CountryId",
                table: "Authors",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Countries_CountryId",
                table: "Authors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Authors_AuthorId",
                table: "Genres",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Countries_CountryId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Authors_AuthorId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_AuthorId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CountryId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Authors");
        }
    }
}
