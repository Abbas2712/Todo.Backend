using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTodoListConfiguraion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "todoLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 14, 23, 12, 48, 932, DateTimeKind.Local).AddTicks(4488),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 485, DateTimeKind.Local).AddTicks(2006));

            migrationBuilder.AlterColumn<bool>(
                name: "IsTodayTodo",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsTasked",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ListId",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: "null",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 14, 23, 12, 48, 929, DateTimeKind.Local).AddTicks(6452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 483, DateTimeKind.Local).AddTicks(4251));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "todoLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 485, DateTimeKind.Local).AddTicks(2006),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 14, 23, 12, 48, 932, DateTimeKind.Local).AddTicks(4488));

            migrationBuilder.AlterColumn<bool>(
                name: "IsTodayTodo",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTasked",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ListId",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: "null",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 12, 15, 41, 37, 483, DateTimeKind.Local).AddTicks(4251),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 14, 23, 12, 48, 929, DateTimeKind.Local).AddTicks(6452));
        }
    }
}
