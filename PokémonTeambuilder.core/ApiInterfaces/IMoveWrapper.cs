using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface IMoveWrapper
    {
        Task<List<Move>> GetAllMovesAsync();
    }
}
