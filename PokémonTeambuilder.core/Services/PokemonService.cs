using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
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
            ValidatePokemon(pokemon, true);
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
                selectedAbilitySlot = 1,
                EVs = standardEVs,
                IVs = standardIVs,
                Gender = standardGender,
                Level = standardLevel,
                Nature = nature,
                Nickname = basePokemon.Name,
            };
            pokemon = await pokemonRepos.CreatePokemonAsync(pokemon);
            ValidatePokemon(pokemon, true);
            return pokemon;
        }

        public async Task DeletePokemonAsync(int id)
        {
            await pokemonRepos.DeletePokemonAsync(id);
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            ValidatePokemon(pokemon, false);
            await pokemonRepos.UpdatePokemonAsync(pokemon);
        }

        private void ValidatePokemon(Pokemon pokemon, bool ValidateBasePokemon)
        {
            if (pokemon.Id <= 0)
                throw new InvalidIdException("Pokemon Id cannot be" + pokemon.BasePokemon.Id, pokemon.BasePokemon.Id, pokemon.GetType());
            if (pokemon.Nature == null)
                throw new InvalidVariableException("Pokemon Nature cannot be null", typeof(Nature));
            if (pokemon.EVs == null)
                throw new InvalidVariableException("Pokemon EVs cannot be null", typeof(Stats));
            if (pokemon.IVs == null)
                throw new InvalidVariableException("Pokemon IVs cannot be null", typeof(Stats));
            if (pokemon.SelectedMoves == null)
                pokemon.SelectedMoves = [];

            if (pokemon.BasePokemon.Id <= 0)
                throw new InvalidIdException("BasePokemon Id cannot be" + pokemon.BasePokemon.Id, pokemon.BasePokemon.Id, pokemon.GetType());
            if (string.IsNullOrEmpty(pokemon.BasePokemon.Name))
                throw new InvalidNameException("BasePokemon Name cannot be null or empty", pokemon.BasePokemon.Name, pokemon.GetType());
            if (pokemon.BasePokemon.Typings == null)
                throw new InvalidVariableException("Pokemon Typing cannot be null", pokemon.BasePokemon.Typings, pokemon.GetType());
            if (pokemon.BasePokemon.Typings.Count <= 0)
                throw new InvalidAmountException("Pokemon cannot have no typing", pokemon.BasePokemon.Typings.Count, new Range(1, int.MaxValue));
            if (pokemon.BasePokemon.BaseStats == null)
                throw new InvalidVariableException("Pokemon Stats cannot be null", pokemon.BasePokemon.BaseStats, typeof(BasePokemon));
            if (pokemon.BasePokemon.BaseStats.Hp <= 0)
                throw new InvalidVariableException("Pokemon Hp cannot be" + pokemon.BasePokemon.BaseStats.Hp, pokemon.BasePokemon.BaseStats.Hp, pokemon.BasePokemon.BaseStats.GetType());
            if (pokemon.BasePokemon.BaseStats.Attack <= 0)
                throw new InvalidVariableException("Pokemon Attack cannot be" + pokemon.BasePokemon.BaseStats.Attack, pokemon.BasePokemon.BaseStats.Attack, pokemon.BasePokemon.BaseStats.GetType());
            if (pokemon.BasePokemon.BaseStats.Defense <= 0)
                throw new InvalidVariableException("Pokemon Defense cannot be" + pokemon.BasePokemon.BaseStats.Defense, pokemon.BasePokemon.BaseStats.Defense, pokemon.BasePokemon.BaseStats.GetType());
            if (pokemon.BasePokemon.BaseStats.SpecialAttack <= 0)
                throw new InvalidVariableException("Pokemon SpecialAttack cannot be" + pokemon.BasePokemon.BaseStats.SpecialAttack, pokemon.BasePokemon.BaseStats.SpecialAttack, pokemon.BasePokemon.BaseStats.GetType());
            if (pokemon.BasePokemon.BaseStats.SpecialDefense <= 0)
                throw new InvalidVariableException("Pokemon SpecialDefense cannot be" + pokemon.BasePokemon.BaseStats.SpecialDefense, pokemon.BasePokemon.BaseStats.SpecialDefense, pokemon.BasePokemon.BaseStats.GetType());
            if (pokemon.BasePokemon.BaseStats.Speed <= 0)
                throw new InvalidVariableException("Pokemon Speed cannot be" + pokemon.BasePokemon.BaseStats.Speed, pokemon.BasePokemon.BaseStats.Speed, pokemon.BasePokemon.BaseStats.GetType());
            if (pokemon.BasePokemon.Abilities == null)
                throw new InvalidVariableException("Pokemon Abilities cannot be null", pokemon.BasePokemon.Abilities, pokemon.GetType());
            if (pokemon.BasePokemon.Abilities.Count <= 0)
                throw new InvalidAmountException("Pokemon cannot have no abilities", pokemon.BasePokemon.Abilities.Count, new Range(1, int.MaxValue));
        }
    }
}
