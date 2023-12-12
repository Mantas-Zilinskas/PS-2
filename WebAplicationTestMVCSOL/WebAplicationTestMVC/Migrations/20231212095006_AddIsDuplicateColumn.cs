using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicationTestMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDuplicateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "StudySets",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "StudySets");
        }
    }
}
