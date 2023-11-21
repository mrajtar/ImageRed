using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRed.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorAndIsPrivateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isPrivate",
                table: "Pictures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "isPrivate",
                table: "Pictures");
        }
    }
}
