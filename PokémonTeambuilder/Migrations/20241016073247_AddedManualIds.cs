using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddedManualIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonTypings_Typings_TypingsId",
                table: "BasePokemonTypings");

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Typings",
                table: "Typings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Typings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Typings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Typings",
                table: "Typings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings",
                column: "BasePokemonsId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonTypings_Typings_TypingsId",
                table: "BasePokemonTypings",
                column: "TypingsId",
                principalTable: "Typings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves",
                column: "TypingId",
                principalTable: "Typings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Natures_NatureId",
                table: "Pokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Natures",
                table: "Natures");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Natures");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Natures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Natures",
                table: "Natures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Natures_NatureId",
                table: "Pokemons",
                column: "NatureId",
                principalTable: "Natures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSelectedMoves_Moves_SelectedMovesId",
                table: "PokemonSelectedMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonMoves_Moves_MovesId",
                table: "BasePokemonMoves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moves",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Moves");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Moves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moves",
                table: "Moves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves",
                column: "TypingId",
                principalTable: "Typings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSelectedMoves_Moves_SelectedMovesId",
                table: "PokemonSelectedMoves",
                column: "SelectedMovesId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonMoves_Moves_MovesId",
                table: "BasePokemonMoves",
                column: "MovesId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Items_ItemId",
                table: "Pokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Items_ItemId",
                table: "Pokemons",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonMoves_BasePokemons_BasePokemonsId",
                table: "BasePokemonMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_BasePokemons_BasePokemonId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonAbilities_BasePokemons_BasePokemonsId",
                table: "BasePokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasePokemons",
                table: "BasePokemons");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasePokemons");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BasePokemons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasePokemons",
                table: "BasePokemons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonAbilities_BasePokemons_BasePokemonsId",
                table: "BasePokemonAbilities",
                column: "BasePokemonsId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_BasePokemons_BasePokemonId",
                table: "Pokemons",
                column: "BasePokemonId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonMoves_BasePokemons_BasePokemonsId",
                table: "BasePokemonMoves",
                column: "BasePokemonsId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings",
                column: "BasePokemonsId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Abilities_AbilityId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonAbilities_Abilities_AbilitiesId",
                table: "BasePokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Abilities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Abilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonAbilities_Abilities_AbilitiesId",
                table: "BasePokemonAbilities",
                column: "AbilitiesId",
                principalTable: "Abilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Abilities_AbilityId",
                table: "Pokemons",
                column: "AbilityId",
                principalTable: "Abilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonTypings_Typings_TypingsId",
                table: "BasePokemonTypings");

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Typings",
                table: "Typings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Typings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Typings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Typings",
                table: "Typings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings",
                column: "BasePokemonsId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonTypings_Typings_TypingsId",
                table: "BasePokemonTypings",
                column: "TypingsId",
                principalTable: "Typings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves",
                column: "TypingId",
                principalTable: "Typings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Natures_NatureId",
                table: "Pokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Natures",
                table: "Natures");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Natures");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Natures",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Natures",
                table: "Natures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Natures_NatureId",
                table: "Pokemons",
                column: "NatureId",
                principalTable: "Natures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSelectedMoves_Moves_SelectedMovesId",
                table: "PokemonSelectedMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonMoves_Moves_MovesId",
                table: "BasePokemonMoves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moves",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Moves");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Moves",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moves",
                table: "Moves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Typings_TypingId",
                table: "Moves",
                column: "TypingId",
                principalTable: "Typings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSelectedMoves_Moves_SelectedMovesId",
                table: "PokemonSelectedMoves",
                column: "SelectedMovesId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonMoves_Moves_MovesId",
                table: "BasePokemonMoves",
                column: "MovesId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Items_ItemId",
                table: "Pokemons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Items",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Items_ItemId",
                table: "Pokemons",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonMoves_BasePokemons_BasePokemonsId",
                table: "BasePokemonMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_BasePokemons_BasePokemonId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonAbilities_BasePokemons_BasePokemonsId",
                table: "BasePokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasePokemons",
                table: "BasePokemons");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasePokemons");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BasePokemons",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasePokemons",
                table: "BasePokemons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonAbilities_BasePokemons_BasePokemonId",
                table: "BasePokemonAbilities",
                column: "BasePokemonId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_BasePokemons_BasePokemonId",
                table: "Pokemons",
                column: "BasePokemonId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonMoves_BasePokemons_BasePokemonId",
                table: "BasePokemonMoves",
                column: "BasePokemonId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonTypings_BasePokemons_BasePokemonsId",
                table: "BasePokemonTypings",
                column: "BasePokemonsId",
                principalTable: "BasePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Abilities_AbilityId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_BasePokemonAbilities_Abilities_AbilitiesId",
                table: "BasePokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Abilities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Abilities",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasePokemonAbilities_Abilities_AbilitiesId",
                table: "BasePokemonAbilities",
                column: "AbilitiesId",
                principalTable: "Abilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Abilities_AbilityId",
                table: "Pokemons",
                column: "AbilityId",
                principalTable: "Abilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
