using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreatedPlansTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Tours_TourId",
                table: "Plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plan",
                table: "Plan");

            migrationBuilder.RenameTable(
                name: "Plan",
                newName: "Plans");

            migrationBuilder.RenameIndex(
                name: "IX_Plan_TourId",
                table: "Plans",
                newName: "IX_Plans_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plans",
                table: "Plans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Tours_TourId",
                table: "Plans",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Tours_TourId",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plans",
                table: "Plans");

            migrationBuilder.RenameTable(
                name: "Plans",
                newName: "Plan");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_TourId",
                table: "Plan",
                newName: "IX_Plan_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plan",
                table: "Plan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Tours_TourId",
                table: "Plan",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
