using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicationTestMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivedStudySets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "StudyTime",
                table: "StudySets",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "ArchivedStudySets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Color = table.Column<int>(type: "INTEGER", nullable: false),
                    StudySetName = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateArchived = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StudyTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    OriginalStudySetId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudySetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedStudySets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedStudySets_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteStudySets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    StudySetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteStudySets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteStudySets_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedStudySets_StudySetId",
                table: "ArchivedStudySets",
                column: "StudySetId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteStudySets_StudySetId",
                table: "FavoriteStudySets",
                column: "StudySetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedStudySets");

            migrationBuilder.DropTable(
                name: "FavoriteStudySets");

            migrationBuilder.DropColumn(
                name: "StudyTime",
                table: "StudySets");
        }
    }
}
