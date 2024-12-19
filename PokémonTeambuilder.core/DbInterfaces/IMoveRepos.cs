using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IMoveRepos
    {
        Task<List<Move>> GetAllMovesAsync();
        Task<int> GetMoveCountAsync();
        Task SetAllMovesAsync(List<Move> moves);
    }
}
