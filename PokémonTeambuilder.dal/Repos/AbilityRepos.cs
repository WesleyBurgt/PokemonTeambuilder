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

        public async Task<List<Ability>> GetAllAbilities()
        {
            List<Ability> abilities = await context.Abilties.ToListAsync();
            return abilities;
        }

        public void SetAllAbilities(List<Ability> abilities)
        {
            foreach (Ability ability in abilities)
            {
                if (context.Abilties.Any(a => a.Id == ability.Id))
                {
                    context.Abilties.Update(ability);
                }
                else
                {
                    context.Abilties.Add(ability);
                }
            }
            context.SaveChanges();
        }
    }
}
