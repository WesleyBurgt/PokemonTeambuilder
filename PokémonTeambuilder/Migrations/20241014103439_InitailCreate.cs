using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.Migrations
{
    /// <inheritdoc />
    public partial class InitailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
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
                    table.PrimaryKey("PK_Item", x => x.Id);
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
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    BasePokemonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasePokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseStatsId = table.Column<int>(type: "int", nullable: false),
                    Sprite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    NatureId = table.Column<int>(type: "int", nullable: true),
                    AbilityId = table.Column<int>(type: "int", nullable: true),
                    EVsId = table.Column<int>(type: "int", nullable: true),
                    IVsId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasePokemons_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemons_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemons_Natures_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Natures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasePokemons_Stats_BaseStatsId",
                        column: x => x.BaseStatsId,
                        principalTable: "Stats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasePokemons_Stats_EVsId",
                        column: x => x.EVsId,
                        principalTable: "Stats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasePokemons_Stats_IVsId",
                        column: x => x.IVsId,
                        principalTable: "Stats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasePokemons_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Typing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePokemonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Typing_BasePokemons_BasePokemonId",
                        column: x => x.BasePokemonId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypingId = table.Column<int>(type: "int", nullable: false),
                    BasePower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    BasePokemonId = table.Column<int>(type: "int", nullable: true),
                    PokemonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_BasePokemons_BasePokemonId",
                        column: x => x.BasePokemonId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Move_BasePokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "BasePokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Move_Typing_TypingId",
                        column: x => x.TypingId,
                        principalTable: "Typing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ability_BasePokemonId",
                table: "Ability",
                column: "BasePokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_AbilityId",
                table: "BasePokemons",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_BaseStatsId",
                table: "BasePokemons",
                column: "BaseStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_EVsId",
                table: "BasePokemons",
                column: "EVsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_ItemId",
                table: "BasePokemons",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_IVsId",
                table: "BasePokemons",
                column: "IVsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_NatureId",
                table: "BasePokemons",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_BasePokemons_TeamId",
                table: "BasePokemons",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_BasePokemonId",
                table: "Move",
                column: "BasePokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_PokemonId",
                table: "Move",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_TypingId",
                table: "Move",
                column: "TypingId");

            migrationBuilder.CreateIndex(
                name: "IX_Typing_BasePokemonId",
                table: "Typing",
                column: "BasePokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ability_BasePokemons_BasePokemonId",
                table: "Ability",
                column: "BasePokemonId",
                principalTable: "BasePokemons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ability_BasePokemons_BasePokemonId",
                table: "Ability");

            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "Typing");

            migrationBuilder.DropTable(
                name: "BasePokemons");

            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Natures");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
