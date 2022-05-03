using Microsoft.EntityFrameworkCore.Migrations;

namespace Boovey.Data.Migrations
{
    public partial class ImplementingIAccessibleInterfaceInCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Countries");
        }
    }
}
