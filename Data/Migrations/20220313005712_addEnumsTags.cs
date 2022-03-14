using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    public partial class addEnumsTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_questionId",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "questionId",
                table: "Answer",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_questionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "TagsQuestion",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "TagsQuestion",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Answer",
                newName: "questionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                newName: "IX_Answer_questionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_questionId",
                table: "Answer",
                column: "questionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
