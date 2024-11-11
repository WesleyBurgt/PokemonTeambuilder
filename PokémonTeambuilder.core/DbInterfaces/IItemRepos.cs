using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IItemRepos
    {
        Task<List<Item>> GetAllItemsAsync();
        Task SetAllItemsAsync(List<Item> items);
    }
}
