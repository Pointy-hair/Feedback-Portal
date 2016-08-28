using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackPortal.Data.Migrations
{
    public partial class ProjectSlugToKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Projects",
                newName: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Projects",
                newName: "Slug");
        }
    }
}
