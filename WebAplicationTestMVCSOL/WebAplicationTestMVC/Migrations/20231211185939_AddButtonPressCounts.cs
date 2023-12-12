using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicationTestMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddButtonPressCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoodButtonPressCount",
                table: "StudySets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BadButtonPressCount",
                table: "StudySets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodButtonPressCount",
                table: "StudySets");

            migrationBuilder.DropColumn(
                name: "BadButtonPressCount",
                table: "StudySets");
        }

    }
}
