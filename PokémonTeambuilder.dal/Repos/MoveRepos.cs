using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
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

        public async Task<List<Move>> GetAllMovesAsync()
        {
            List<Move> moves = await context.Moves.ToListAsync();
            return moves;
        }

        public async Task SetAllMovesAsync(List<Move> moves)
        {
            foreach (Move move in moves)
            {
                Typing typing = move.Typing;
                Typing? existingTyping = await context.Typings.FirstOrDefaultAsync(t => t.Id == typing.Id);

                if (existingTyping != null)
                {
                    move.Typing = existingTyping;
                }
                else
                {
                    throw new ReposResponseException("Typing is not in database");
                }

                if (await context.Moves.AnyAsync(m => m.Id == move.Id))
                {
                    context.Moves.Update(move);
                }
                else
                {
                    context.Moves.Add(move);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
