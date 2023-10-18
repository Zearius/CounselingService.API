using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CounselingService.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dscription",
                table: "SpecialEvents",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SpecialEvents",
                newName: "Dscription");
        }
    }
}
