using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Natures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Up = table.Column<int>(type: "int", nullable: true),
                    Down = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Typings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasePokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseStatsId = table.Column<int>(type: "int", nullable: false),
                    Sprite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasePokemons_Stats_BaseStatsId",
                        column: x => x.BaseStatsId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasePokemonAbilities",
                columns: table => new
                {
                    AbilitiesId = table.Column<int>(type: "int", nullable: false),
                    BasePokemonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonAbilities", x => new { x.AbilitiesId, x.BasePokemonsId });
                    table.ForeignKey(
                        name: "FK_BasePokemonAbilities_Abilities_AbilitiesId",
                        column: x => x.AbilitiesId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemonAbilities_BasePokemons_BasePokemonsId",
                        column: x => x.BasePokemonsId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasePokemonTypings",
                columns: table => new
                {
                    BasePokemonsId = table.Column<int>(type: "int", nullable: false),
                    TypingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonTypings", x => new { x.BasePokemonsId, x.TypingsId });
                    table.ForeignKey(
                        name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                        column: x => x.BasePokemonsId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemonTypings_Typings_TypingsId",
                        column: x => x.TypingsId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    PokemonDtoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePokemonId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    NatureId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false),
                    EVsId = table.Column<int>(type: "int", nullable: false),
                    IVsId = table.Column<int>(type: "int", nullable: false),
                    TeamDtoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.PokemonDtoId);
                    table.ForeignKey(
                        name: "FK_Pokemons_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_BasePokemons_BasePokemonId",
                        column: x => x.BasePokemonId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Natures_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Natures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Stats_EVsId",
                        column: x => x.EVsId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_Stats_IVsId",
                        column: x => x.IVsId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_Teams_TeamDtoId",
                        column: x => x.TeamDtoId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BasePokemonMoves",
                columns: table => new
                {
                    BasePokemonsId = table.Column<int>(type: "int", nullable: false),
                    MovesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonMoves", x => new { x.BasePokemonsId, x.MovesId });
                    table.ForeignKey(
                        name: "FK_BasePokemonMoves_BasePokemons_BasePokemonsId",
                        column: x => x.BasePokemonsId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemonMoves_Moves_MovesId",
                        column: x => x.MovesId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSelectedMoves",
                columns: table => new
                {
                    PokemonsPokemonDtoId = table.Column<int>(type: "int", nullable: false),
                    SelectedMovesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSelectedMoves", x => new { x.PokemonsPokemonDtoId, x.SelectedMovesId });
                    table.ForeignKey(
                        name: "FK_PokemonSelectedMoves_Moves_SelectedMovesId",
                        column: x => x.SelectedMovesId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonSelectedMoves_Pokemons_PokemonsPokemonDtoId",
                        column: x => x.PokemonsPokemonDtoId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonDtoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonAbilities_BasePokemonsId",
                table: "BasePokemonAbilities",
                column: "BasePokemonsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonMoves_MovesId",
                table: "BasePokemonMoves",
                column: "MovesId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_BaseStatsId",
                table: "BasePokemons",
                column: "BaseStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonTypings_TypingsId",
                table: "BasePokemonTypings",
                column: "TypingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_TypingId",
                table: "Moves",
                column: "TypingId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AbilityId",
                table: "Pokemons",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_BasePokemonId",
                table: "Pokemons",
                column: "BasePokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_EVsId",
                table: "Pokemons",
                column: "EVsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_ItemId",
                table: "Pokemons",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_IVsId",
                table: "Pokemons",
                column: "IVsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_NatureId",
                table: "Pokemons",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_TeamDtoId",
                table: "Pokemons",
                column: "TeamDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSelectedMoves_SelectedMovesId",
                table: "PokemonSelectedMoves",
                column: "SelectedMovesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasePokemonAbilities");

            migrationBuilder.DropTable(
                name: "BasePokemonMoves");

            migrationBuilder.DropTable(
                name: "BasePokemonTypings");

            migrationBuilder.DropTable(
                name: "PokemonSelectedMoves");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Typings");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "BasePokemons");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Natures");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Stats");
        }
    }
}
