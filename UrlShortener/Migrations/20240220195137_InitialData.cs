using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "url_management",
                columns: new[] { "ShortUrl", "OriginalUrl" },
                values: new object[,]
                {
                    { "HtyU83f", "https://code-maze.com/ultimate-aspnetcore-webapi-second-edition/?source=nav" },
                    { "YtyB45f", "https://stackoverflow.com/questions/tagged/c%23" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "url_management",
                keyColumn: "ShortUrl",
                keyValue: "HtyU83f");

            migrationBuilder.DeleteData(
                table: "url_management",
                keyColumn: "ShortUrl",
                keyValue: "YtyB45f");
        }
    }
}
