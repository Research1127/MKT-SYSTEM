using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mktsystem.infrastructure.Migrations
{
    public partial class RemoveOldClassColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Students",
                type: "text",
                nullable: true);
        }
    }
}