using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MyDbAddPriorityAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ToDoItems",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TaskPriority",
                table: "ToDoItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "TaskPriority",
                table: "ToDoItems");
        }
    }
}
