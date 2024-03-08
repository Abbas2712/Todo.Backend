using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditedTodoItemAndTodoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoItems_todoLists_TodoListId",
                table: "todoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_todoItems",
                table: "todoItems");

            migrationBuilder.RenameTable(
                name: "todoItems",
                newName: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "TodoListId",
                table: "TodoItems",
                newName: "ListId");

            migrationBuilder.RenameIndex(
                name: "IX_todoItems_TodoListId",
                table: "TodoItems",
                newName: "IX_TodoItems_ListId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TodoItems",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsImportant",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TodoItems",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 6, 23, 0, 56, 767, DateTimeKind.Local).AddTicks(1633),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_TodoList",
                table: "TodoItems",
                column: "ListId",
                principalTable: "todoLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_TodoList",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems");

            migrationBuilder.RenameTable(
                name: "TodoItems",
                newName: "todoItems");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "todoItems",
                newName: "TodoListId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_ListId",
                table: "todoItems",
                newName: "IX_todoItems_TodoListId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "todoItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<bool>(
                name: "IsImportant",
                table: "todoItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "todoItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "todoItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "todoItems",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 6, 23, 0, 56, 767, DateTimeKind.Local).AddTicks(1633));

            migrationBuilder.AddPrimaryKey(
                name: "PK_todoItems",
                table: "todoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_todoItems_todoLists_TodoListId",
                table: "todoItems",
                column: "TodoListId",
                principalTable: "todoLists",
                principalColumn: "Id");
        }
    }
}
