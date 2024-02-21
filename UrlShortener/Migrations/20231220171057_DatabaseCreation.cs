using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "url_management",
                columns: table => new
                {
                    ShortUrl = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    OriginalUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url_management", x => x.ShortUrl);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "url_management");
        }
    }
}
