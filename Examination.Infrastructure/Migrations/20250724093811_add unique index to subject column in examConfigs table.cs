using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduniqueindextosubjectcolumninexamConfigstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamConfigurations_SubjectId",
                table: "ExamConfigurations");

            migrationBuilder.CreateIndex(
                name: "IX_ExamConfigurations_SubjectId",
                table: "ExamConfigurations",
                column: "SubjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamConfigurations_SubjectId",
                table: "ExamConfigurations");

            migrationBuilder.CreateIndex(
                name: "IX_ExamConfigurations_SubjectId",
                table: "ExamConfigurations",
                column: "SubjectId");
        }
    }
}
