using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApp_Data.Migrations
{
    public partial class block : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserExamsId",
                table: "UserAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_UserExamsId",
                table: "UserAnswers",
                column: "UserExamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_UserExams_UserExamsId",
                table: "UserAnswers",
                column: "UserExamsId",
                principalTable: "UserExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_UserExams_UserExamsId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_UserExamsId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "UserExamsId",
                table: "UserAnswers");
        }
    }
}
