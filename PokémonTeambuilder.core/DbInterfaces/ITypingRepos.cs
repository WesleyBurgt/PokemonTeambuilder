using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface ITypingRepos
    {
        Task<List<Typing>> GetAllTypingsAsync();
        Task<int> GetTypingCountAsync();
        Task SetAllTypingsAsync(List<Typing> typings);

    }
}
