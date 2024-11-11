using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IMoveRepos
    {
        Task<List<Move>> GetAllMovesAsync();
        Task SetAllMovesAsync(List<Move> moves);
    }
}
