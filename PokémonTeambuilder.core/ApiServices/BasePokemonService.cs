using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
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
                RemoveDuplicateAbilities(pokemon);
                ValidateBasePokemon(pokemon);
            }
            await pokemonRepos.SetBasePokemonListAsync(basePokemons);
        }

        private void ValidateBasePokemon(BasePokemon pokemon)
        {
            if (pokemon.Id <= 0)
                throw new InvalidIdException("Pokemon Id cannot be" + pokemon.Id, pokemon.Id, pokemon.GetType());
            if (string.IsNullOrEmpty(pokemon.Name))
                throw new InvalidNameException("Pokemon Name cannot be null or empty", pokemon.Name, pokemon.GetType());
            if (pokemon.Typings == null)
                throw new InvalidVariableException("Pokemon Typing cannot be null", pokemon.Typings, pokemon.GetType());
            if (pokemon.Typings.Count <= 0)
                throw new InvalidAmountException("Pokemon cannot have no typing", pokemon.Typings.Count, new Range(1, int.MaxValue));
            if (pokemon.BaseStats == null)
                throw new InvalidVariableException("Pokemon Stats cannot be null", pokemon.BaseStats, typeof(BasePokemon));
            if (pokemon.BaseStats.Hp <= 0)
                throw new InvalidVariableException("Pokemon Hp cannot be" + pokemon.BaseStats.Hp, pokemon.BaseStats.Hp, pokemon.BaseStats.GetType());
            if (pokemon.BaseStats.Attack <= 0)
                throw new InvalidVariableException("Pokemon Attack cannot be" + pokemon.BaseStats.Attack, pokemon.BaseStats.Attack, pokemon.BaseStats.GetType());
            if (pokemon.BaseStats.Defense <= 0)
                throw new InvalidVariableException("Pokemon Defense cannot be" + pokemon.BaseStats.Defense, pokemon.BaseStats.Defense, pokemon.BaseStats.GetType());
            if (pokemon.BaseStats.SpecialAttack <= 0)
                throw new InvalidVariableException("Pokemon SpecialAttack cannot be" + pokemon.BaseStats.SpecialAttack, pokemon.BaseStats.SpecialAttack, pokemon.BaseStats.GetType());
            if (pokemon.BaseStats.SpecialDefense <= 0)
                throw new InvalidVariableException("Pokemon SpecialDefense cannot be" + pokemon.BaseStats.SpecialDefense, pokemon.BaseStats.SpecialDefense, pokemon.BaseStats.GetType());
            if (pokemon.BaseStats.Speed <= 0)
                throw new InvalidVariableException("Pokemon Speed cannot be" + pokemon.BaseStats.Speed, pokemon.BaseStats.Speed, pokemon.BaseStats.GetType());
            if (pokemon.Abilities == null)
                throw new InvalidVariableException("Pokemon Abilities cannot be null", pokemon.Abilities, pokemon.GetType());
            if (pokemon.Abilities.Count <= 0)
                throw new InvalidAmountException("Pokemon cannot have no abilities", pokemon.Abilities.Count, new Range(1, int.MaxValue));
        }

        private bool HasDuplicateAbilities(BasePokemon pokemon)
        {
            return pokemon.Abilities.GroupBy(ability => ability.AbilityId).Any(group => group.Count() > 1);
        }

        private void RemoveDuplicateAbilities(BasePokemon pokemon)
        {
            if (HasDuplicateAbilities(pokemon))
            {
                pokemon.Abilities = pokemon.Abilities.GroupBy(ability => ability.AbilityId)
                    .Select(group => group.OrderBy(item => item.Slot).FirstOrDefault()).ToList();
            }
        }
    }
}
