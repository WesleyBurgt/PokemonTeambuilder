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

        public async Task<List<BasePokemon>> GetBasePokemonListAsync(int offset, int limit)
        {
            List<BasePokemon> basePokemons = await context.BasePokemons.ToListAsync();
            return basePokemons;
        }

        public async Task SetBasePokemonListAsync(List<BasePokemon> basePokemons)
        {
            foreach (BasePokemon basePokemon in basePokemons)
            {
                List<Typing> typingsList = basePokemon.Typings.ToList();
                for (int i = 0; i < typingsList.Count; i++)
                {
                    Typing typing = typingsList[i];

                    Typing? existingTyping = await context.Typings.FirstOrDefaultAsync(t => t.Id == typing.Id);

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

                List<BasePokemonAbility> abilityList = basePokemon.Abilities.ToList();
                for (int i = 0; i < abilityList.Count; i++)
                {
                    BasePokemonAbility ability = abilityList[i];

                    BasePokemonAbility? existingAbility = await context.BasePokemonAbilities.FirstOrDefaultAsync(a => a.AbilityId == ability.AbilityId && a.BasePokemonId == ability.BasePokemonId);

                    if (existingAbility != null)
                    {
                        abilityList[i] = existingAbility;
                    }
                    else
                    {
                        throw new Exception("BasePokemonAbility is not in database");
                    }
                }
                basePokemon.Abilities = abilityList;

                if (await context.BasePokemons.AnyAsync(bp => bp.Id == basePokemon.Id))
                {
                    context.BasePokemons.Update(basePokemon);
                }
                else
                {
                    context.BasePokemons.Add(basePokemon);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
