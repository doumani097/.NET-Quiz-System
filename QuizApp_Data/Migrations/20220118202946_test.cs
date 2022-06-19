using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApp_Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_UserExams_UserExamsId",
                table: "UserAnswers");

            migrationBuilder.RenameColumn(
                name: "UserExamsId",
                table: "UserAnswers",
                newName: "UserExamId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_UserExamsId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_UserExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_UserExams_UserExamId",
                table: "UserAnswers",
                column: "UserExamId",
                principalTable: "UserExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_UserExams_UserExamId",
                table: "UserAnswers");

            migrationBuilder.RenameColumn(
                name: "UserExamId",
                table: "UserAnswers",
                newName: "UserExamsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_UserExamId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_UserExamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_UserExams_UserExamsId",
                table: "UserAnswers",
                column: "UserExamsId",
                principalTable: "UserExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
