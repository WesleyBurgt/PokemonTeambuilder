using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface ITypingWrapper
    {
        Task<List<Typing>> GetAllTypingsAsync();
    }
}
