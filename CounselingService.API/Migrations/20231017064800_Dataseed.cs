using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CounselingService.API.Migrations
{
    /// <inheritdoc />
    public partial class Dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Counselings",
                columns: new[] { "Id", "Counselor", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Catherine Forestrad", "Bi-weekly group, designed to support those recovering from Gambling Addiction", "Gambelers Anonymous" },
                    { 2, "Brian Brackett", "Bi-weekly group, designed to support those recovering from Narcotics Addiction.", "Narcotics Anonymous" },
                    { 3, "Sarah Johnson", "Bi-weekly group, designed to support those recovering from Alcohol Addiction.", "Alcholics Anonymous" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Counselings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Counselings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Counselings",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
