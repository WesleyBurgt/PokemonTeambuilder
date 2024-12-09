using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface INatureRepos
    {
        Task<List<Nature>> GetAllNaturesAsync();
        Task<int> GetNatureCountAsync();
        Task<Nature> GetNatureByIdAsync(int id);
        Task SetAllNaturesAsync(List<Nature> natures);
    }
}
