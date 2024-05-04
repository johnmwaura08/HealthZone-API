using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthZoneAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_status_to_todo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Todo",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Todo");
        }
    }
}
