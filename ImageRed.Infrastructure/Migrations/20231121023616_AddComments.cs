using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageRed.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Pictures_PictureId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Pictures_PictureId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PictureId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PictureId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PictureId",
                table: "Ratings",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PictureId",
                table: "Comments",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Pictures_PictureId",
                table: "Comments",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Pictures_PictureId",
                table: "Ratings",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id");
        }
    }
}
