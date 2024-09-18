using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteForCauseFinal.Migrations
{
    public partial class AddingSignFunctionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CausePostSigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CausePostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CausePostSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CausePostSigns_CausePosts_CausePostId",
                        column: x => x.CausePostId,
                        principalTable: "CausePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CausePostSigns_CausePostId",
                table: "CausePostSigns",
                column: "CausePostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CausePostSigns");
        }
    }
}
