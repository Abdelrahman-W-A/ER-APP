using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses_Recorder_App.Migrations
{
    /// <inheritdoc />
    public partial class AddingQuestionColumnsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserQuestion",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserQuestionAnswer",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserQuestion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserQuestionAnswer",
                table: "Users");
        }
    }
}
