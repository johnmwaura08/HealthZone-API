using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthZoneAPI.Migrations
{
    /// <inheritdoc />
    public partial class update_workout_category_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryType",
                table: "WorkoutCategory",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryType",
                table: "WorkoutCategory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
