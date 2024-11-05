using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class ItemRepos : IItemRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public ItemRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            List<Item> items = await context.Items.ToListAsync();
            return items;
        }

        public async Task SetAllItemsAsync(List<Item> items)
        {
            foreach (Item item in items)
            {
                if (await context.Items.AnyAsync(i => i.Id == item.Id))
                {
                    context.Items.Update(item);
                }
                else
                {
                    context.Items.Add(item);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
