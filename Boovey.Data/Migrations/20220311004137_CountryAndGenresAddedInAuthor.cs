using Microsoft.EntityFrameworkCore.Migrations;

namespace Boovey.Data.Migrations
{
    public partial class CountryAndGenresAddedInAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuthorsGenres",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsGenres", x => new { x.AuthorId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AuthorsGenres_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorsGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CountryId",
                table: "Authors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsGenres_GenreId",
                table: "AuthorsGenres",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Countries_CountryId",
                table: "Authors",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Countries_CountryId",
                table: "Authors");

            migrationBuilder.DropTable(
                name: "AuthorsGenres");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CountryId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Authors");
        }
    }
}
