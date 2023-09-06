using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetBlazor.Server.Migrations
{
    /// <inheritdoc />
    public partial class TodoProgressAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Todo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Todo");
        }
    }
}
