using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiServices
{
    public class MoveService
    {
        private readonly IMoveWrapper moveWrapper;
        private readonly IMoveRepos moveRepos;

        public MoveService(IMoveWrapper moveWrapper, IMoveRepos moveRepos)
        {
            this.moveWrapper = moveWrapper;
            this.moveRepos = moveRepos;
        }

        public async Task FetchAndSaveMovesAsync()
        {
            List<Move> moves = await moveWrapper.GetAllMovesAsync();
            foreach (Move move in moves)
            {
                ValidateMove(move);
            }
            await moveRepos.SetAllMovesAsync(moves);
        }

        private void ValidateMove(Move move)
        {
            if (move.Id <= 0)
                throw new InvalidIdException("Move Id cannot be" + move.Id, move.Id, move.GetType());
            if (string.IsNullOrEmpty(move.Name))
                throw new InvalidNameException("Move Name cannot be null or empty", move.Name, move.GetType());
        }
    }
}
