using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteForCauseFinal.Migrations
{
    public partial class AddingComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CausePostComment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CausePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CausePostComment", x => x.id);
                    table.ForeignKey(
                        name: "FK_CausePostComment_CausePosts_CausePostId",
                        column: x => x.CausePostId,
                        principalTable: "CausePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CausePostComment_CausePostId",
                table: "CausePostComment",
                column: "CausePostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CausePostComment");
        }
    }
}
