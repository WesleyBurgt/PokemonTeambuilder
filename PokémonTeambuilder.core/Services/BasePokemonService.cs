using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.Services
{
    public class BasePokemonService
    {
        private readonly IBasePokemonWrapper pokemonWrapper;
        public BasePokemonService(IBasePokemonWrapper pokemonWrapper)
        {
            this.pokemonWrapper = pokemonWrapper;
        }

        public async Task<List<BasePokemon>> GetPokemonList(int offset, int limit)
        {
            List<BasePokemon> list = await pokemonWrapper.GetPokemonList(offset, limit);
            foreach (BasePokemon pokemon in list)
            {
                //TODO: make custom Exceptions
                if (pokemon.Id <= 0)
                    throw new Exception("Pokemon Id cannot be" + pokemon.Id);
                if (String.IsNullOrEmpty(pokemon.Name))
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
            return list;
        }
    }
}
