using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class birthday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birhtday",
                table: "Contacts",
                newName: "BirthDay");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "Contacts",
                newName: "Birhtday");
        }
    }
}
