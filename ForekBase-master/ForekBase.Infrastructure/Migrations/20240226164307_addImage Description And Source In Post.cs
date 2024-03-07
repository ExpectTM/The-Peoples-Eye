using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addImageDescriptionAndSourceInPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageDescription_1",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSource_1",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDescription_1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageSource_1",
                table: "Posts");
        }
    }
}
