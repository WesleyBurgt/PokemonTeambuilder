using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IItemRepos
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<int> GetItemCountAsync();
        Task SetAllItemsAsync(List<Item> items);
    }
}
