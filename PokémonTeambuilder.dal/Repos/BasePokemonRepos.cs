using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class BasePokemonRepos : IBasePokemonRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public BasePokemonRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<BasePokemon>> GetBasePokemonListAsync(int offset, int limit)
        {
            List<BasePokemon> basePokemons = await context.BasePokemons
                .Include(bp => bp.Typings)
                    .ThenInclude(t => t.Relationships)
                        .ThenInclude(tr => tr.RelatedTyping)
                .Include(bp => bp.Abilities)
                    .ThenInclude(a => a.Ability)
                .Include(bp => bp.Moves)
                    .ThenInclude(m => m.Typing)
                        .ThenInclude(t => t.Relationships)
                            .ThenInclude(tr => tr.RelatedTyping)
                .Include(bp => bp.BaseStats)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return basePokemons;
        }

        public async Task<int> GetBasePokemonCountAsync()
        {
            int count = await context.BasePokemons.CountAsync();
            return count;
        }

        public async Task SetBasePokemonListAsync(List<BasePokemon> basePokemons)
        {
            foreach (BasePokemon basePokemon in basePokemons)
            {
                if (BasePokemonIsInDatabase(basePokemon))
                {
                    await UpdateExistingBasePokemon(basePokemon);
                }
                else
                {
                    await AddBasePokemon(basePokemon);
                }
            }
            await context.SaveChangesAsync();
        }

        private bool BasePokemonIsInDatabase(BasePokemon basePokemon)
        {
            return context.BasePokemons.Any(bp => bp.Id == basePokemon.Id);
        }

        private async Task AddBasePokemon(BasePokemon basePokemon)
        {
            if (!BasePokemonIsInDatabase(basePokemon))
            {
                List<Typing> typings = await GetTypingsFromDatabase(basePokemon);
                List<BasePokemonAbility> basePokemonAbilities = await GetBasePokemonAbilitiesFromDatabase(basePokemon);
                List<Move> moves = await GetMovesFromDatabase(basePokemon);

                basePokemon.Typings = typings;
                basePokemon.Abilities = basePokemonAbilities;
                basePokemon.Moves = moves;

                context.BasePokemons.Add(basePokemon);
            }
        }

        private async Task UpdateExistingBasePokemon(BasePokemon basePokemon)
        {
            BasePokemon? existingBasePokemon = await context.BasePokemons
                .Include(bp => bp.Typings)
                .Include(bp => bp.Abilities)
                .Include(bp => bp.Moves)
                .Include(bp => bp.BaseStats)
                .FirstOrDefaultAsync(bp => bp.Id == basePokemon.Id);

            if (existingBasePokemon != null)
            {
                List<Typing> typings = await GetTypingsFromDatabase(basePokemon);
                List<BasePokemonAbility> basePokemonAbilities = await GetBasePokemonAbilitiesFromDatabase(basePokemon);
                List<Move> moves = await GetMovesFromDatabase(basePokemon);

                existingBasePokemon.Typings = typings;
                existingBasePokemon.Abilities = basePokemonAbilities;
                UpdateMovesExistingBasePokemon(existingBasePokemon, moves);

                context.BasePokemons.Update(existingBasePokemon);
            }
        }

        private void UpdateMovesExistingBasePokemon(BasePokemon existingBasePokemon, List<Move> moves)
        {
            existingBasePokemon.Moves = existingBasePokemon.Moves
                .Where(em => moves.Any(m => m.Id == em.Id))
                .ToList();
            List<Move> newMoves = moves.Where(m => !existingBasePokemon.Moves.Any(em => em.Id == m.Id)).ToList();
            foreach (Move move in newMoves)
            {
                existingBasePokemon.Moves.Add(move);
            }
        }

        private async Task<List<Typing>> GetTypingsFromDatabase(BasePokemon basePokemon)
        {
            List<Typing> typingList = basePokemon.Typings.ToList();
            for (int i = 0; i < typingList.Count; i++)
            {
                Typing typing = typingList[i];

                Typing? existingTyping = await context.Typings.FirstOrDefaultAsync(t => t.Id == typing.Id);

                if (existingTyping != null)
                {
                    typingList[i] = existingTyping;
                }
                else
                {
                    throw new Exception("Typing is not in database");
                }
            }
            return typingList;
        }

        private async Task<List<BasePokemonAbility>> GetBasePokemonAbilitiesFromDatabase(BasePokemon basePokemon)
        {
            List<BasePokemonAbility> basePokemonAbilityList = basePokemon.Abilities.ToList();
            for (int i = 0; i < basePokemonAbilityList.Count; i++)
            {
                BasePokemonAbility basePokemonAbility = basePokemonAbilityList[i];

                BasePokemonAbility? existingBasePokemonAbility = await context.BasePokemonAbilities.FirstOrDefaultAsync(a => a.AbilityId == basePokemonAbility.AbilityId && a.BasePokemonId == basePokemonAbility.BasePokemonId);

                if (existingBasePokemonAbility != null)
                {
                    basePokemonAbilityList[i] = existingBasePokemonAbility;
                }
                else
                {
                    Ability? existingAbility = context.Abilties.FirstOrDefault(a => a.Id == basePokemonAbility.AbilityId);
                    if (existingAbility != null)
                    {
                        basePokemonAbility.Ability = existingAbility;
                    }
                    else
                    {
                        throw new Exception("Ability is not in database");
                    }
                }
            }
            return basePokemonAbilityList;
        }

        private async Task<List<Move>> GetMovesFromDatabase(BasePokemon basePokemon)
        {
            List<Move> moveList = basePokemon.Moves.ToList();
            for (int i = 0; i < moveList.Count; i++)
            {
                Move move = moveList[i];

                Move? existingMove = await context.Moves.FirstOrDefaultAsync(m => m.Id == move.Id);

                if (existingMove != null)
                {
                    moveList[i] = existingMove;
                }
                else
                {
                    throw new Exception("Move is not in database");
                }
            }
            return moveList;
        }
    }
}
