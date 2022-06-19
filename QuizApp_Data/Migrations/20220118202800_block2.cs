using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApp_Data.Migrations
{
    public partial class block2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoTakes",
                table: "UserAnswers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoTakes",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
