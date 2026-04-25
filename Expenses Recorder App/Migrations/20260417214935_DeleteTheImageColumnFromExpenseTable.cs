using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses_Recorder_App.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTheImageColumnFromExpenseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptImageURL",
                table: "Expenses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiptImageURL",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
