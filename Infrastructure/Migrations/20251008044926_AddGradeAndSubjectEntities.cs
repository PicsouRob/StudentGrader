using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeAndSubjectEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Grade",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Exam",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade1",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade2",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade3",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade4",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Grade",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalGrade",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentId",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_SubjectId",
                table: "Grade",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Subject_SubjectId",
                table: "Grade",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Subject_SubjectId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_StudentId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_SubjectId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Exam",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Grade1",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Grade2",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Grade3",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Grade4",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "TotalGrade",
                table: "Grade");
        }
    }
}
