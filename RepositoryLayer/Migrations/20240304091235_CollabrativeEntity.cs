using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class CollabrativeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaborativeEntities",
                columns: table => new
                {
                    CollaborativeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborativeEmail = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    NotesId = table.Column<int>(nullable: false),
                    CollabratedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsTrash = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborativeEntities", x => x.CollaborativeId);
                    table.ForeignKey(
                        name: "FK_CollaborativeEntities_UserTable_Id",
                        column: x => x.Id,
                        principalTable: "UserTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollaborativeEntities_NotesTable_NotesId",
                        column: x => x.NotesId,
                        principalTable: "NotesTable",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaborativeEntities_Id",
                table: "CollaborativeEntities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborativeEntities_NotesId",
                table: "CollaborativeEntities",
                column: "NotesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaborativeEntities");
        }
    }
}
