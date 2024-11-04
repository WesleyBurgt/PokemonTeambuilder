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

        public async Task<List<Item>> GetAllItems()
        {
            List<Item> items = await context.Items.ToListAsync();
            return items;
        }

        public async void SetAllItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                if (context.Items.Any(i => i.Id == item.Id))
                {
                    context.Items.Update(item);
                }
                else
                {
                    context.Items.Add(item);
                }
            }
            context.SaveChanges();
        }
    }
}
