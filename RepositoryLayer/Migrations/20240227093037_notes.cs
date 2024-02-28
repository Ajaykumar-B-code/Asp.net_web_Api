using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "UserTable",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "UserTable",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserTable",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "NotesTable",
                columns: table => new
                {
                    NotesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Reminder = table.Column<DateTime>(nullable: false),
                    Colour = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsArchive = table.Column<bool>(nullable: false),
                    isPin = table.Column<bool>(nullable: false),
                    isTrash = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTable", x => x.NotesId);
                    table.ForeignKey(
                        name: "FK_NotesTable_UserTable_Id",
                        column: x => x.Id,
                        principalTable: "UserTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_Id",
                table: "NotesTable",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesTable");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserTable",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserTable",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserTable",
                newName: "id");
        }
    }
}
