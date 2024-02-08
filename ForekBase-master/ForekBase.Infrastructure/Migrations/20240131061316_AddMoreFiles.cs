using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekBase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostPicture",
                table: "Posts",
                newName: "ThirdFile");

            migrationBuilder.AddColumn<string>(
                name: "FirstFile",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondFile",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstFile",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SecondFile",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "ThirdFile",
                table: "Posts",
                newName: "PostPicture");
        }
    }
}
