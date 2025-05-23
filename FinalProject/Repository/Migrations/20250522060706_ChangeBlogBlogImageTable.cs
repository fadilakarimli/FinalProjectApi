using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBlogBlogImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages");

            migrationBuilder.CreateIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages",
                column: "BlogId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages");

            migrationBuilder.CreateIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages",
                column: "BlogId");
        }
    }
}
