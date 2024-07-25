using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaginationLearningFietkau.Migrations
{
    /// <inheritdoc />
    public partial class TypoFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Perishables",
                newName: "PerishableID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PerishableID",
                table: "Perishables",
                newName: "ID");
        }
    }
}
