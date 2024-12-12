using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSelectedMovePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedMove_Pokemons_PokemonId",
                table: "SelectedMove");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectedMove",
                table: "SelectedMove");

            migrationBuilder.DropIndex(
                name: "IX_SelectedMove_PokemonId",
                table: "SelectedMove");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SelectedMove");

            migrationBuilder.RenameTable(
                name: "SelectedMove",
                newName: "SelectedMoves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectedMoves",
                table: "SelectedMoves",
                columns: new[] { "PokemonId", "Slot" });

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedMoves_Pokemons_PokemonId",
                table: "SelectedMoves",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedMoves_Pokemons_PokemonId",
                table: "SelectedMoves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectedMoves",
                table: "SelectedMoves");

            migrationBuilder.RenameTable(
                name: "SelectedMoves",
                newName: "SelectedMove");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SelectedMove",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectedMove",
                table: "SelectedMove",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMove_PokemonId",
                table: "SelectedMove",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedMove_Pokemons_PokemonId",
                table: "SelectedMove",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
