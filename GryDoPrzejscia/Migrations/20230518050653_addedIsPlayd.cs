using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GryDoPrzejscia.Migrations
{
    /// <inheritdoc />
    public partial class addedIsPlayd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPlayed",
                table: "GameList",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPlayed",
                table: "GameList");
        }
    }
}
