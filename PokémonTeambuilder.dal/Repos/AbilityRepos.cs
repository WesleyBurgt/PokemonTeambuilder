using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class AbilityRepos : IAbilityRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public AbilityRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Ability>> GetAllAbilitiesAsync()
        {
            List<Ability> abilities = await context.Abilties.ToListAsync();
            return abilities;
        }

        public async Task SetAllAbilitiesAsync(List<Ability> abilities)
        {
            foreach (Ability ability in abilities)
            {
                if (await context.Abilties.AnyAsync(a => a.Id == ability.Id))
                {
                    context.Abilties.Update(ability);
                }
                else
                {
                    context.Abilties.Add(ability);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
