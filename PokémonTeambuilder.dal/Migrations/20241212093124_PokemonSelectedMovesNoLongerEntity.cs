using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    /// <inheritdoc />
    public partial class PokemonSelectedMovesNoLongerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovePokemon");

            migrationBuilder.CreateTable(
                name: "SelectedMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedMove_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedMove_PokemonId",
                table: "SelectedMove",
                column: "PokemonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedMove");

            migrationBuilder.CreateTable(
                name: "MovePokemon",
                columns: table => new
                {
                    PokemonsId = table.Column<int>(type: "int", nullable: false),
                    SelectedMovesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovePokemon", x => new { x.PokemonsId, x.SelectedMovesId });
                    table.ForeignKey(
                        name: "FK_MovePokemon_Moves_SelectedMovesId",
                        column: x => x.SelectedMovesId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovePokemon_Pokemons_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovePokemon_SelectedMovesId",
                table: "MovePokemon",
                column: "SelectedMovesId");
        }
    }
}
