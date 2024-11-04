using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IMoveRepos
    {
        Task<List<Move>> GetAllMoves();
        void SetAllMoves(List<Move> moves);
    }
}
