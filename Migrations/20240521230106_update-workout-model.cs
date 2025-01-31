﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthZoneAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateworkoutmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workout");

            migrationBuilder.AddColumn<int>(
                name: "WeightInPounds",
                table: "Workout",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightInPounds",
                table: "Workout");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workout",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
