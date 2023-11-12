using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ImageRed.Infrastructure.Migrations.ImageRedAuthDb
{
    /// <inheritdoc />
    public partial class FixSeedDbWithRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38a3610d-5ba0-494e-bb2e-983c11fcc711", "38a3610d-5ba0-494e-bb2e-983c11fcc711", "Admin", "ADMIN" },
                    { "da13d7fa-ca91-4207-9f91-6452408b0e4d", "da13d7fa-ca91-4207-9f91-6452408b0e4d", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38a3610d-5ba0-494e-bb2e-983c11fcc711");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da13d7fa-ca91-4207-9f91-6452408b0e4d");
        }
    }
}
