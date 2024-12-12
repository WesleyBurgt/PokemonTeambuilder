using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSelectedMovePrimaryKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectedMoves",
                table: "SelectedMoves");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SelectedMoves",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectedMoves",
                table: "SelectedMoves",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMoves_PokemonId",
                table: "SelectedMoves",
                column: "PokemonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectedMoves",
                table: "SelectedMoves");

            migrationBuilder.DropIndex(
                name: "IX_SelectedMoves_PokemonId",
                table: "SelectedMoves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SelectedMoves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectedMoves",
                table: "SelectedMoves",
                columns: new[] { "PokemonId", "Slot" });
        }
    }
}
