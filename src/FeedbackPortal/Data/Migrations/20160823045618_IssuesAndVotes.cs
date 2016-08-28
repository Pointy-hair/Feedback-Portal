using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FeedbackPortal.Data.Migrations
{
    public partial class IssuesAndVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    Severity = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "IssueVotes",
                columns: table => new
                {
                    IssueId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    VoteType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueVotes", x => new { x.IssueId, x.UserId });
                    table.ForeignKey(
                        name: "FK_IssueVotes_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_CreatedUserId",
                table: "Issues",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueVotes_IssueId",
                table: "IssueVotes",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueVotes_UserId",
                table: "IssueVotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueVotes");

            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
