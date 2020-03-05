using Microsoft.EntityFrameworkCore.Migrations;

namespace GrendelData.Migrations
{
    public partial class AddIsSessionActiveToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsQuestionActive",
                table: "Questions",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSessionActive",
                table: "Questions",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsQuestionActive",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsSessionActive",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Questions",
                type: "boolean",
                nullable: true,
                defaultValue: false);
        }
    }
}
