using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddsomeExamconfigurationstomakesurenotwoexamsareactiveinthesametime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exams_StudentId",
                table: "Exams");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StudentId_SubjectId",
                table: "Exams",
                columns: new[] { "StudentId", "SubjectId" },
                unique: true,
                filter: "[status] = 'InProgress'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exams_StudentId_SubjectId",
                table: "Exams");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StudentId",
                table: "Exams",
                column: "StudentId");
        }
    }
}
