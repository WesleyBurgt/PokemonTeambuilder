using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class MoveRepos : IMoveRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public MoveRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Move>> GetAllMoves()
        {
            List<Move> moves = await context.Moves.ToListAsync();
            return moves;
        }

        public async void SetAllMoves(List<Move> moves)
        {
            foreach (Move move in moves)
            {
                if (context.Moves.Any(m => m.Id == move.Id))
                {
                    Typing typing = move.Typing;

                    var existingTyping = context.Typings.FirstOrDefault(t => t.Id == typing.Id);

                    if (existingTyping != null)
                    {
                        move.Typing = existingTyping;
                    }
                    else
                    {
                        throw new Exception("Typing is not in database");
                    }

                    context.Moves.Update(move);
                }
                else
                {
                    context.Moves.Add(move);
                }
            }
            context.SaveChanges();
        }
    }
}
