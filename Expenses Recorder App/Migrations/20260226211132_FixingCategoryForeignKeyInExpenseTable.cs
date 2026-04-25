using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses_Recorder_App.Migrations
{
    /// <inheritdoc />
    public partial class FixingCategoryForeignKeyInExpenseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryNameId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryNameId",
                table: "Expenses",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryNameId",
                table: "Expenses",
                newName: "IX_Expenses_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Expenses",
                newName: "CategoryNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                newName: "IX_Expenses_CategoryNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryNameId",
                table: "Expenses",
                column: "CategoryNameId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
