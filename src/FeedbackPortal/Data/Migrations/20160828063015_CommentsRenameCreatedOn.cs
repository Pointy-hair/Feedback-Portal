using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackPortal.Data.Migrations
{
    public partial class CommentsRenameCreatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("CreatedOn", "Comments", "CreatedOnUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("CreatedOnUtc", "Comments", "CreatedOn");
        }
    }
}
