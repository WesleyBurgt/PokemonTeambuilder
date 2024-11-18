using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class ItemService
    {
        private readonly IItemRepos itemRepos;
        public ItemService(IItemRepos itemRepos)
        {
            this.itemRepos = itemRepos;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            List<Item> items = [];
            try
            {
                items = await itemRepos.GetAllItemsAsync();
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Items", ex);
            }

            foreach (Item item in items)
            {
                ValidateItem(item);
            }
            return items;
        }

        public async Task<int> GetItemCountAsync()
        {
            try
            {
                int result = await itemRepos.GetItemCountAsync();
                return result;
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Item count", ex);
            }
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
