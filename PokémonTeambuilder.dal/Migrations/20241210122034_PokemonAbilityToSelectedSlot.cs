using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    /// <inheritdoc />
    public partial class PokemonAbilityToSelectedSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_BasePokemonAbilities_AbilityBasePokemonId_AbilityId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_AbilityBasePokemonId_AbilityId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "AbilityBasePokemonId",
                table: "Pokemons");

            migrationBuilder.RenameColumn(
                name: "AbilityId",
                table: "Pokemons",
                newName: "selectedAbilitySlot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "selectedAbilitySlot",
                table: "Pokemons",
                newName: "AbilityId");

            migrationBuilder.AddColumn<int>(
                name: "AbilityBasePokemonId",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_AbilityBasePokemonId_AbilityId",
                table: "Pokemons",
                columns: new[] { "AbilityBasePokemonId", "AbilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_BasePokemonAbilities_AbilityBasePokemonId_AbilityId",
                table: "Pokemons",
                columns: new[] { "AbilityBasePokemonId", "AbilityId" },
                principalTable: "BasePokemonAbilities",
                principalColumns: new[] { "BasePokemonId", "AbilityId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
