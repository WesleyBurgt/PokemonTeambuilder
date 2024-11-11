using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface INatureWrapper
    {
        Task<List<Nature>> GetAllNaturesAsync();
    }
}
