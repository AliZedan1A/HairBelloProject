using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerSide.Migrations
{
    /// <inheritdoc />
    public partial class BarberUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartBreack",
                table: "Barbers",
                newName: "StartBreak");

            migrationBuilder.RenameColumn(
                name: "EndBreack",
                table: "Barbers",
                newName: "EndBreak");

            migrationBuilder.AddColumn<bool>(
                name: "IsInBreak",
                table: "Barbers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInBreak",
                table: "Barbers");

            migrationBuilder.RenameColumn(
                name: "StartBreak",
                table: "Barbers",
                newName: "StartBreack");

            migrationBuilder.RenameColumn(
                name: "EndBreak",
                table: "Barbers",
                newName: "EndBreack");
        }
    }
}
