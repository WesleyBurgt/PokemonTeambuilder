using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false),
                    BasePower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false)
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
                name: "TypingRelationships",
                columns: table => new
                {
                    TypingId = table.Column<int>(type: "int", nullable: false),
                    RelatedTypingId = table.Column<int>(type: "int", nullable: false),
                    Relation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingRelationships", x => new { x.TypingId, x.RelatedTypingId });
                    table.ForeignKey(
                        name: "FK_TypingRelationships_Typings_RelatedTypingId",
                        column: x => x.RelatedTypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypingRelationships_Typings_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasePokemonAbilities",
                columns: table => new
                {
                    BasePokemonId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonAbilities", x => new { x.BasePokemonId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_BasePokemonAbilities_Abilties_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasePokemonAbilities_BasePokemons_BasePokemonId",
                        column: x => x.BasePokemonId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "BasePokemonMove",
                columns: table => new
                {
                    BasePokemonsId = table.Column<int>(type: "int", nullable: false),
                    MovesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemonMove", x => new { x.BasePokemonsId, x.MovesId });
                    table.ForeignKey(
                        name: "FK_BasePokemonMove_BasePokemons_BasePokemonsId",
                        column: x => x.BasePokemonsId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemonMove_Moves_MovesId",
                        column: x => x.MovesId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePokemonId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    NatureId = table.Column<int>(type: "int", nullable: false),
                    AbilityBasePokemonId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false),
                    EVsId = table.Column<int>(type: "int", nullable: false),
                    IVsId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_BasePokemonAbilities_AbilityBasePokemonId_AbilityId",
                        columns: x => new { x.AbilityBasePokemonId, x.AbilityId },
                        principalTable: "BasePokemonAbilities",
                        principalColumns: new[] { "BasePokemonId", "AbilityId" },
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
                        principalColumn: "Id");
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
                        name: "FK_Pokemons_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

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
                name: "IX_BasePokemonAbilities_AbilityId",
                table: "BasePokemonAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonMove_MovesId",
                table: "BasePokemonMove",
                column: "MovesId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_BaseStatsId",
                table: "BasePokemons",
                column: "BaseStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemonTyping_TypingsId",
                table: "BasePokemonTyping",
                column: "TypingsId");

            migrationBuilder.CreateIndex(
                name: "IX_MovePokemon_SelectedMovesId",
                table: "MovePokemon",
                column: "SelectedMovesId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_TypingId",
                table: "Moves",
                column: "TypingId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AbilityBasePokemonId_AbilityId",
                table: "Pokemons",
                columns: new[] { "AbilityBasePokemonId", "AbilityId" });

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
                name: "IX_Pokemons_TeamId",
                table: "Pokemons",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingRelationships_RelatedTypingId",
                table: "TypingRelationships",
                column: "RelatedTypingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasePokemonMove");

            migrationBuilder.DropTable(
                name: "BasePokemonTyping");

            migrationBuilder.DropTable(
                name: "MovePokemon");

            migrationBuilder.DropTable(
                name: "TypingRelationships");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Typings");

            migrationBuilder.DropTable(
                name: "BasePokemonAbilities");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Natures");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Abilties");

            migrationBuilder.DropTable(
                name: "BasePokemons");

            migrationBuilder.DropTable(
                name: "Stats");
        }
    }
}
