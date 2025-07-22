using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExamStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "InProgress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Exams");
        }
    }
}
