using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiServices
{
    public class BasePokemonService
    {
        private readonly IBasePokemonWrapper pokemonWrapper;
        private readonly IBasePokemonRepos pokemonRepos;

        public BasePokemonService(IBasePokemonWrapper pokemonWrapper, IBasePokemonRepos pokemonRepos)
        {
            this.pokemonWrapper = pokemonWrapper;
            this.pokemonRepos = pokemonRepos;
        }

        public async Task FetchAndSaveBasePokemonsAsync()
        {
            List<BasePokemon> basePokemons = await pokemonWrapper.GetAllBasePokemonsAsync();
            foreach (BasePokemon pokemon in basePokemons)
            {
                ValidateBasePokemon(pokemon);
            }
            await pokemonRepos.SetBasePokemonListAsync(basePokemons);
        }

        private void ValidateBasePokemon(BasePokemon pokemon)
        {
            //TODO: make custom Exceptions
            if (pokemon.Id <= 0)
                throw new Exception("Pokemon Id cannot be" + pokemon.Id);
            if (string.IsNullOrEmpty(pokemon.Name))
                throw new Exception("Pokemon Name cannot be null or empty");
            if (pokemon.Typings.Count <= 0)
                throw new Exception("Pokemon cannot have no typing");
            if (pokemon.BaseStats.Hp <= 0)
                throw new Exception("Pokemon Hp cannot be" + pokemon.BaseStats.Hp);
            if (pokemon.BaseStats.Attack <= 0)
                throw new Exception("Pokemon Attack cannot be" + pokemon.BaseStats.Attack);
            if (pokemon.BaseStats.Defense <= 0)
                throw new Exception("Pokemon Defense cannot be" + pokemon.BaseStats.Defense);
            if (pokemon.BaseStats.SpecialAttack <= 0)
                throw new Exception("Pokemon SpecialAttack cannot be" + pokemon.BaseStats.SpecialAttack);
            if (pokemon.BaseStats.SpecialDefense <= 0)
                throw new Exception("Pokemon SpecialDefense cannot be" + pokemon.BaseStats.SpecialDefense);
            if (pokemon.BaseStats.Speed <= 0)
                throw new Exception("Pokemon Speed cannot be" + pokemon.BaseStats.Speed);
            if (pokemon.Abilities.Count <= 0)
                throw new Exception("Pokemon cannot have no abilities");
        }
    }
}
