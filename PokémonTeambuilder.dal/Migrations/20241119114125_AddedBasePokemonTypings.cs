using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    /// <inheritdoc />
    public partial class AddedBasePokemonTypings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasePokemonTyping");

            migrationBuilder.CreateTable(
                name: "BasePokemonTypings",
                columns: table => new
                {
                    BasePokemonId = table.Column<int>(type: "int", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonTypings", x => new { x.BasePokemonId, x.TypingId });
                    table.ForeignKey(
                        name: "FK_BasePokemonTypings_BasePokemons_BasePokemonId",
                        column: x => x.BasePokemonId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasePokemonTypings_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonTypings_TypingId",
                table: "BasePokemonTypings",
                column: "TypingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasePokemonTypings");

            migrationBuilder.CreateTable(
                name: "BasePokemonTyping",
                columns: table => new
                {
                    BasePokemonsId = table.Column<int>(type: "int", nullable: false),
                    TypingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonTyping", x => new { x.BasePokemonsId, x.TypingsId });
                    table.ForeignKey(
                        name: "FK_BasePokemonTyping_BasePokemons_BasePokemonsId",
                        column: x => x.BasePokemonsId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemonTyping_Typings_TypingsId",
                        column: x => x.TypingsId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonTyping_TypingsId",
                table: "BasePokemonTyping",
                column: "TypingsId");
        }
    }
}
