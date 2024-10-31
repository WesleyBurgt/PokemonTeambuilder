using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface ITypingRepos
    {
        Task<List<Typing>> GetAllTypings();
        void SetAllTypings(List<Typing> typings);

    }
}
