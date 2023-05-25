using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GryDoPrzejscia.Migrations
{
    /// <inheritdoc />
    public partial class DefaultValuesInCheckbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isPlayed",
                table: "GameList",
                newName: "Gram");

            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "GameList",
                newName: "Przeszedłem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Przeszedłem",
                table: "GameList",
                newName: "IsFinished");

            migrationBuilder.RenameColumn(
                name: "Gram",
                table: "GameList",
                newName: "isPlayed");
        }
    }
}
