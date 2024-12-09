using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class PokemonService
    {
        private readonly IPokemonRepos pokemonRepos;
        private readonly IBasePokemonRepos basePokemonRepos;
        private readonly INatureRepos natureRepos;

        public PokemonService(IPokemonRepos pokemonRepos, IBasePokemonRepos basePokemonRepos, INatureRepos natureRepos)
        {
            this.pokemonRepos = pokemonRepos;
            this.basePokemonRepos = basePokemonRepos;
            this.natureRepos = natureRepos;
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            Pokemon pokemon = await pokemonRepos.GetPokemonByIdAsync(id);
            return pokemon;
        }

        public async Task<Pokemon> CreatePokemonAsync(int basePokemonId)
        {
            BasePokemon basePokemon = await basePokemonRepos.GetBasePokemonByIdAsync(basePokemonId);
            Nature nature = await natureRepos.GetNatureByIdAsync(1);

            Stats standardEVs = new Stats
            {
                Hp = 0,
                Attack = 0,
                Defense = 0,
                SpecialAttack = 0,
                SpecialDefense = 0,
                Speed = 0
            };
            Stats standardIVs = new Stats
            {
                Hp = 31,
                Attack = 31,
                Defense = 31,
                SpecialAttack = 31,
                SpecialDefense = 31,
                Speed = 31
            };
            Gender standardGender = Gender.Male;
            int standardLevel = 100;

            Pokemon pokemon = new Pokemon
            {
                BasePokemon = basePokemon,
                Ability = basePokemon.Abilities.FirstOrDefault(),
                EVs = standardEVs,
                IVs = standardIVs,
                Gender = standardGender,
                Level = standardLevel,
                Nature = nature,
                Nickname = basePokemon.Name,
            };
            pokemon = await pokemonRepos.CreatePokemonAsync(pokemon);
            return pokemon;
        }

        public async Task DeletePokemonAsync(int id)
        {
            await pokemonRepos.DeletePokemonAsync(id);
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            await pokemonRepos.UpdatePokemonAsync(pokemon);
        }

        private void ValidatePokemon(Pokemon pokemon)
        {
            //TODO: Validate Pokemon
        }
    }
}
