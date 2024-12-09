using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;
using System.Collections.Generic;

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
                    .ThenInclude(bpt => bpt.Typing)
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

        public async Task<BasePokemon> GetBasePokemonByIdAsync(int id)
        {
            BasePokemon basePokemon = await context.BasePokemons
                .Include(bp => bp.Typings)
                    .ThenInclude(bpt => bpt.Typing)
                        .ThenInclude(t => t.Relationships)
                            .ThenInclude(tr => tr.RelatedTyping)
                .Include(bp => bp.Abilities)
                    .ThenInclude(a => a.Ability)
                .Include(bp => bp.Moves)
                    .ThenInclude(m => m.Typing)
                        .ThenInclude(t => t.Relationships)
                            .ThenInclude(tr => tr.RelatedTyping)
                .Include(bp => bp.BaseStats)
                .FirstOrDefaultAsync(basePokemon => basePokemon.Id == id);
            return basePokemon;
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
                List<BasePokemonTyping> basePokemonTypings = await GetBasePokemonTypingsFromDatabase(basePokemon);
                List<BasePokemonAbility> basePokemonAbilities = await GetBasePokemonAbilitiesFromDatabase(basePokemon);
                List<Move> moves = await GetMovesFromDatabase(basePokemon);

                basePokemon.Typings = basePokemonTypings;
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
                List<BasePokemonTyping> basePokemonTypings = await GetBasePokemonTypingsFromDatabase(basePokemon);
                List<BasePokemonAbility> basePokemonAbilities = await GetBasePokemonAbilitiesFromDatabase(basePokemon);
                List<Move> moves = await GetMovesFromDatabase(basePokemon);

                existingBasePokemon.Typings = basePokemonTypings;
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

        private async Task<List<BasePokemonTyping>> GetBasePokemonTypingsFromDatabase(BasePokemon basePokemon)
        {
            List<BasePokemonTyping> basePokemonTypingList = basePokemon.Typings.ToList();
            for (int i = 0; i < basePokemonTypingList.Count; i++)
            {
                BasePokemonTyping basePokemonTyping = basePokemonTypingList[i];

                BasePokemonTyping? existingBasePokemonTyping = await context.BasePokemonTypings.FirstOrDefaultAsync(bpt => bpt.TypingId == basePokemonTyping.TypingId);

                if (existingBasePokemonTyping != null)
                {
                    basePokemonTypingList[i] = existingBasePokemonTyping;
                }
                else
                {
                    Typing? existingTyping = context.Typings.FirstOrDefault(t => t.Id == basePokemonTyping.TypingId);
                    if (existingTyping != null)
                    {
                        basePokemonTyping.Typing = existingTyping;
                    }
                    else
                    {
                        throw new ReposResponseException("Typing is not in database");
                    }
                }
            }
            return basePokemonTypingList;
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
                        throw new ReposResponseException("Ability is not in database");
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
                    throw new ReposResponseException("Move is not in database");
                }
            }
            return moveList;
        }
    }
}
