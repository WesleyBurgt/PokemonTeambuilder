using PokémonTeambuilder.core.DbInterfaces;
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
            catch (Exception ex)
            {
                throw new Exception("could not get Items");
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
            catch (Exception ex)
            {
                throw new Exception("could not get Item count");
            }
        }

        private void ValidateItem(Item item)
        {
            //TODO: make custom Exceptions
            if (item.Id <= 0)
                throw new Exception("Item Id cannot be" + item.Id);
            if (string.IsNullOrEmpty(item.Name))
                throw new Exception("Item Name cannot be null or empty");
        }
    }
}
