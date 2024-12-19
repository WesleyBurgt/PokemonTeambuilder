using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using System.Collections.Generic;

namespace PokémonTeambuilder.core.Services
{
    public class MoveService
    {
        private readonly IMoveRepos moveRepos;

        public MoveService(IMoveRepos moveRepos)
        {
            this.moveRepos = moveRepos;
        }

        public async Task<List<Move>> GetAllMovesAsync()
        {
            List<Move> moves = [];
            try
            {
                moves = await moveRepos.GetAllMovesAsync();
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Moves", ex);
            }

            foreach (Move move in moves)
            {
                ValidateMove(move);
            }
            return moves;
        }

        public async Task<int> GetMoveCountAsync()
        {
            try
            {
                int count = await moveRepos.GetMoveCountAsync();
                return count;
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Move count", ex);
            }
        }

        private void ValidateMove(Move move)
        {
            if (move.Id <= 0)
                throw new InvalidIdException("Move Id cannot be" + move.Id, move.Id, move.GetType());
            if (string.IsNullOrEmpty(move.Name))
                throw new InvalidNameException("Move Name cannot be null or empty", move.Name, move.GetType());
            if (move.Typing == null)
                throw new InvalidVariableException("Move Typing cannot be null", move.Typing, move.GetType());
            if (move.Accuracy != null && move.Accuracy < 0)
                throw new InvalidAmountException("Move Accuracy cannot be lower than 0", (int)move.Accuracy, new Range(0, int.MaxValue));
            if (move.BasePower != null && move.BasePower < 0)
                throw new InvalidAmountException("Move BasePower cannot be lower than 0", (int)move.BasePower, new Range(0, int.MaxValue));
            if (move.PP != null && move.PP < 0)
                throw new InvalidAmountException("Move PP cannot be lower than 0", (int)move.BasePower, new Range(0, int.MaxValue));
            if (move.Category != "physical" && move.Category != "special" && move.Category != "status")
                throw new InvalidVariableException("Move Category cannot be" + move.Category, move.Category, move.GetType());
        }
    }
}
