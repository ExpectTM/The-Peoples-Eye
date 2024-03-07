using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockqoutefiledescriptionsourceinpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockQuote",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageDescription_2",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageDescription_3",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSource_2",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSource_3",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockQuote",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageDescription_2",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageDescription_3",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageSource_2",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageSource_3",
                table: "Posts");
        }
    }
}
