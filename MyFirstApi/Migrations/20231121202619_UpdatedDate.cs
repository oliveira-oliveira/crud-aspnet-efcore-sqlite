using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Todos",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Todos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Todos",
                newName: "Date");
        }
    }
}
