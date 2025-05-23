using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBlogTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages",
                column: "BlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages",
                column: "BlogId",
                unique: true);
        }
    }
}
