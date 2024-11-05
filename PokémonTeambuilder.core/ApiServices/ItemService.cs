using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
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

        public async Task GetAllItemsAndSaveThem()
        {
            List<Item> items = await itemWrapper.GetAllItems();
            foreach (Item item in items)
            {
                ValidateItem(item);
            }
            await itemRepos.SetAllItemsAsync(items);
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
