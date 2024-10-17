using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Dto;

namespace PokémonTeambuilder.dal
{
    public class BasePokemonRepos : IBasePokemonRepos
    {
        private readonly IAppDbContext appDbContext;
        public BasePokemonRepos(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Task<List<BasePokemonDto>> GetBasePokemonList(int offset, int limit)
        {
            return appDbContext.BasePokemons
                .Skip(offset)
                .Take(limit)
                .Include(bp => bp.Typings)
                    .ThenInclude(t => t.Weaknesses)
                .Include(bp => bp.Typings)
                    .ThenInclude(t => t.Resistances)
                .Include(bp => bp.Typings)
                    .ThenInclude(t => t.Immunities)
                .Include(bp => bp.Abilities)
                .Include(bp => bp.Moves)
                    .ThenInclude(m => m.Typing)
                .Include(bp => bp.BaseStats)
                .ToListAsync();
        }

    }
}
