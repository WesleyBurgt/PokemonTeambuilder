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
            ValidatePokemon(pokemon);
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
            ValidatePokemon(pokemon);
            return pokemon;
        }

        public async Task DeletePokemonAsync(int id)
        {
            await pokemonRepos.DeletePokemonAsync(id);
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            ValidatePokemon(pokemon);
            await pokemonRepos.UpdatePokemonAsync(pokemon);
        }

        private static void ValidatePokemon(Pokemon pokemon)
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
            if (pokemon.BasePokemonId <= 0)
                throw new InvalidIdException("BasePokemon Id cannot be" + pokemon.BasePokemonId, pokemon.BasePokemonId, pokemon.GetType());
            if (pokemon.BasePokemon != null)
                ValidateBasePokemon(pokemon.BasePokemon);
        }

        private static void ValidateBasePokemon(BasePokemon basePokemon)
        {
            if (basePokemon.Id <= 0)
                throw new InvalidIdException("BasePokemon Id cannot be" + basePokemon.Id, basePokemon.Id, basePokemon.GetType());
            if (string.IsNullOrEmpty(basePokemon.Name))
                throw new InvalidNameException("BasePokemon Name cannot be null or empty", basePokemon.Name, basePokemon.GetType());
            if (basePokemon.Typings == null)
                throw new InvalidVariableException("Pokemon Typing cannot be null", basePokemon.Typings, basePokemon.GetType());
            if (basePokemon.Typings.Count <= 0)
                throw new InvalidAmountException("Pokemon cannot have no typing", basePokemon.Typings.Count, new Range(1, int.MaxValue));
            if (basePokemon.BaseStats == null)
                throw new InvalidVariableException("Pokemon Stats cannot be null", basePokemon.BaseStats, typeof(BasePokemon));
            if (basePokemon.BaseStats.Hp <= 0)
                throw new InvalidVariableException("Pokemon Hp cannot be" + basePokemon.BaseStats.Hp, basePokemon.BaseStats.Hp, basePokemon.BaseStats.GetType());
            if (basePokemon.BaseStats.Attack <= 0)
                throw new InvalidVariableException("Pokemon Attack cannot be" + basePokemon.BaseStats.Attack, basePokemon.BaseStats.Attack, basePokemon.BaseStats.GetType());
            if (basePokemon.BaseStats.Defense <= 0)
                throw new InvalidVariableException("Pokemon Defense cannot be" + basePokemon.BaseStats.Defense, basePokemon.BaseStats.Defense, basePokemon.BaseStats.GetType());
            if (basePokemon.BaseStats.SpecialAttack <= 0)
                throw new InvalidVariableException("Pokemon SpecialAttack cannot be" + basePokemon.BaseStats.SpecialAttack, basePokemon.BaseStats.SpecialAttack, basePokemon.BaseStats.GetType());
            if (basePokemon.BaseStats.SpecialDefense <= 0)
                throw new InvalidVariableException("Pokemon SpecialDefense cannot be" + basePokemon.BaseStats.SpecialDefense, basePokemon.BaseStats.SpecialDefense, basePokemon.BaseStats.GetType());
            if (basePokemon.BaseStats.Speed <= 0)
                throw new InvalidVariableException("Pokemon Speed cannot be" + basePokemon.BaseStats.Speed, basePokemon.BaseStats.Speed, basePokemon.BaseStats.GetType());
            if (basePokemon.Abilities == null)
                throw new InvalidVariableException("Pokemon Abilities cannot be null", basePokemon.Abilities, basePokemon.GetType());
            if (basePokemon.Abilities.Count <= 0)
                throw new InvalidAmountException("Pokemon cannot have no abilities", basePokemon.Abilities.Count, new Range(1, int.MaxValue));
        }
    }
}
