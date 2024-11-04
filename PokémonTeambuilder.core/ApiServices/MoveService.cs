using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
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

        public async Task GetAllMovesAndSaveThem()
        {
            List<Move> moves = await moveWrapper.GetAllMoves();
            foreach (Move move in moves)
            {
                ValidateMove(move);
            }
            moveRepos.SetAllMoves(moves);
        }

        private void ValidateMove(Move move)
        {
            //TODO: make custom Exceptions
            if (move.Id <= 0)
                throw new Exception("Move Id cannot be" + move.Id);
            if (string.IsNullOrEmpty(move.Name))
                throw new Exception("Move Name cannot be null or empty");
        }
    }
}
