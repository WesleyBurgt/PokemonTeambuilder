using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface ITypingWrapper
    {
        Task<List<Typing>> GetAllTypings();
    }
}
