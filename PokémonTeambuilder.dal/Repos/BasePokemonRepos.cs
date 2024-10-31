using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class BasePokemonRepos : IBasePokemonRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public BasePokemonRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<BasePokemon>> GetBasePokemonList(int offset, int limit)
        {
            List<BasePokemon> basePokemons = await context.BasePokemons.ToListAsync();
            return basePokemons;
        }

        public void SetBasePokemonList(List<BasePokemon> basePokemons)
        {
            foreach (BasePokemon basePokemon in basePokemons)
            {
                var typingsList = basePokemon.Typings.ToList();
                for (int i = 0; i < typingsList.Count; i++)
                {
                    var typing = typingsList[i];

                    // Get the existing Typing from the database
                    var existingTyping = context.Typings.FirstOrDefault(t => t.Id == typing.Id);

                    if (existingTyping != null)
                    {
                        typingsList[i] = existingTyping;
                    }
                    else
                    {
                        throw new Exception("Typing is not in database");
                    }
                }
                basePokemon.Typings = typingsList;

                var abilityList = basePokemon.Abilities.ToList();
                for (int i = 0; i < abilityList.Count; i++)
                {
                    var ability = abilityList[i];

                    var existingAbility = context.BasePokemonAbilities.FirstOrDefault(a => a.AbilityId == ability.AbilityId && a.BasePokemonId == ability.BasePokemonId);

                    if (existingAbility != null)
                    {
                        abilityList[i] = existingAbility;
                    }
                    else
                    {
                        throw new Exception("Ability is not in database");
                    }
                }
                basePokemon.Abilities = abilityList;

                if (context.BasePokemons.Any(bp => bp.Id == basePokemon.Id))
                {
                    context.BasePokemons.Update(basePokemon);
                }
                else
                {
                    context.BasePokemons.Add(basePokemon);
                }
            }
            context.SaveChanges();
        }
    }
}
