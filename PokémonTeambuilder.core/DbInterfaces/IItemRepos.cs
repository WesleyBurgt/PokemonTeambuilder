using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IItemRepos
    {
        Task<List<Item>> GetAllItems();
        void SetAllItems(List<Item> items);
    }
}
