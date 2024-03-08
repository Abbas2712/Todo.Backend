using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialTodoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "todoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "todoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsImportant = table.Column<bool>(type: "bit", nullable: false),
                    TodoListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_todoItems_todoLists_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "todoLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_todoItems_TodoListId",
                table: "todoItems",
                column: "TodoListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todoItems");

            migrationBuilder.DropTable(
                name: "todoLists");
        }
    }
}
