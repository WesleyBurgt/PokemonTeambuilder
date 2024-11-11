using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class NatureRepos : INatureRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public NatureRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Nature>> GetAllNaturesAsync()
        {
            List<Nature> natures = await context.Natures.ToListAsync();
            return natures;
        }

        public async Task<int> GetNatureCountAsync()
        {
            int count = await context.Natures.CountAsync();
            return count;
        }

        public async Task SetAllNaturesAsync(List<Nature> natures)
        {
            foreach (Nature nature in natures)
            {
                if (await context.Natures.AnyAsync(n => n.Id == nature.Id))
                {
                    context.Natures.Update(nature);
                }
                else
                {
                    context.Natures.Add(nature);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
