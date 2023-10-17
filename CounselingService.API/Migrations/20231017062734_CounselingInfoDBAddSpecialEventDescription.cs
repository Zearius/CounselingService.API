using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CounselingService.API.Migrations
{
    /// <inheritdoc />
    public partial class CounselingInfoDBAddSpecialEventDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dscription",
                table: "SpecialEvents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dscription",
                table: "SpecialEvents");
        }
    }
}
