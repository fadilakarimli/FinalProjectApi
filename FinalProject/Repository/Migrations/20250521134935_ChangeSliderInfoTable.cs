using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSliderInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlidersInfo_Sliders_SliderId",
                table: "SlidersInfo");

            migrationBuilder.DropIndex(
                name: "IX_SlidersInfo_SliderId",
                table: "SlidersInfo");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "SlidersInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "SlidersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SlidersInfo_SliderId",
                table: "SlidersInfo",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlidersInfo_Sliders_SliderId",
                table: "SlidersInfo",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
