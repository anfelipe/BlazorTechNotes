using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorTechNotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameNoteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Notes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "IsPublishedAt",
                table: "Notes",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Notes",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Notes",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Notes",
                newName: "IsPublishedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Notes",
                newName: "CreateAt");
        }
    }
}
