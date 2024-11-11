using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface IItemWrapper
    {
        Task<List<Item>> GetAllItemsAsync();
    }
}
