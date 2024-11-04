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

        public async Task<List<Nature>> GetAllNatures()
        {
            List<Nature> natures = await context.Natures.ToListAsync();
            return natures;
        }

        public async void SetAllNatures(List<Nature> natures)
        {
            foreach (Nature nature in natures)
            {
                if (context.Natures.Any(n => n.Id == nature.Id))
                {
                    context.Natures.Update(nature);
                }
                else
                {
                    context.Natures.Add(nature);
                }
            }
            context.SaveChanges();
        }
    }
}
