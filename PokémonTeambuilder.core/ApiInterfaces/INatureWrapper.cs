using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface INatureWrapper
    {
        Task<List<Nature>> GetAllNatures();
    }
}
