using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizWPF.Migrations
{
    /// <inheritdoc />
    public partial class QuizTableStructureModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Quizzes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
