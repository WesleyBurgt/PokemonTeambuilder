using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface INatureRepos
    {
        Task<List<Nature>> GetAllNatures();
        void SetAllNatures(List<Nature> natures);
    }
}
