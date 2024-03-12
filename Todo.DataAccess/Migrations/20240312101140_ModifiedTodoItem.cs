using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTodoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "todoLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 485, DateTimeKind.Local).AddTicks(2006),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 8, 15, 51, 52, 923, DateTimeKind.Local).AddTicks(9837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 483, DateTimeKind.Local).AddTicks(4251),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 8, 15, 51, 52, 922, DateTimeKind.Local).AddTicks(5738));

            migrationBuilder.AddColumn<bool>(
                name: "IsTodayTodo",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTodayTodo",
                table: "TodoItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "todoLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 8, 15, 51, 52, 923, DateTimeKind.Local).AddTicks(9837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 485, DateTimeKind.Local).AddTicks(2006));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 8, 15, 51, 52, 922, DateTimeKind.Local).AddTicks(5738),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 483, DateTimeKind.Local).AddTicks(4251));
        }
    }
}
