using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiServices
{
    public class ItemService
    {
        private readonly IItemWrapper itemWrapper;
        private readonly IItemRepos itemRepos;

        public ItemService(IItemWrapper itemWrapper, IItemRepos itemRepos)
        {
            this.itemWrapper = itemWrapper;
            this.itemRepos = itemRepos;
        }

        public async Task FetchAndSaveItemsAsync()
        {
            List<Item> items = await itemWrapper.GetAllItemsAsync();
            foreach (Item item in items)
            {
                ValidateItem(item);
            }
            await itemRepos.SetAllItemsAsync(items);
        }

        private void ValidateItem(Item item)
        {
            if (item.Id <= 0)
                throw new InvalidIdException("Item Id cannot be" + item.Id, item.Id, item.GetType());
            if (string.IsNullOrEmpty(item.Name))
                throw new InvalidNameException("Item Name cannot be null or empty", item.Name, item.GetType());
        }
    }
}
