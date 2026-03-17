using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mktsystem.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingOutstandingAndDueAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<decimal>(
                name: "DueAmount",
                table: "Payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingAmount",
                table: "Payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueAmount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount",
                table: "Payments");
            
        }
    }
}
