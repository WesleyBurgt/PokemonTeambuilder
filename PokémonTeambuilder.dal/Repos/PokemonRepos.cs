using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class PokemonRepos : IPokemonRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public PokemonRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Pokemon>> GetPokemonsByTeamIdAsync(int teamId)
        {
            Team team = await context.Teams
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.BasePokemon)
                        .ThenInclude(bp => bp.Typings)
                            .ThenInclude(bt => bt.Typing)
                                .ThenInclude(t => t.Relationships)
                                    .ThenInclude(tr => tr.RelatedTyping)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.BasePokemon)
                        .ThenInclude(bp => bp.Abilities)
                            .ThenInclude(bpa => bpa.Ability)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.BasePokemon)
                        .ThenInclude(bp => bp.BaseStats)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.BasePokemon)
                        .ThenInclude(bp => bp.Moves)
                            .ThenInclude(m => m.Typing)
                                .ThenInclude(t => t.Relationships)
                                    .ThenInclude(tr => tr.RelatedTyping)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.Item)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.Nature)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.SelectedMoves)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.EVs)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.IVs)
                .FirstOrDefaultAsync(team => team.Id == teamId);
            List<Pokemon> pokemons = team.Pokemons.ToList();
            return pokemons;
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            Pokemon pokemon = await context.Pokemons
                .Include(p => p.BasePokemon)
                    .ThenInclude(bp => bp.Typings)
                        .ThenInclude(bt => bt.Typing)
                            .ThenInclude(t => t.Relationships)
                                .ThenInclude(tr => tr.RelatedTyping)
                .Include(p => p.BasePokemon)
                    .ThenInclude(bp => bp.Abilities)
                        .ThenInclude(bpa => bpa.Ability)
                .Include(p => p.BasePokemon)
                    .ThenInclude(bp => bp.BaseStats)
                .Include(p => p.BasePokemon)
                    .ThenInclude(bp => bp.Moves)
                        .ThenInclude(m => m.Typing)
                            .ThenInclude(t => t.Relationships)
                                .ThenInclude(tr => tr.RelatedTyping)
                .Include(p => p.Item)
                .Include(p => p.Nature)
                .Include(p => p.SelectedMoves)
                .Include(p => p.EVs)
                .Include(p => p.IVs)
                .FirstOrDefaultAsync(pokemon => pokemon.Id == id);
            return pokemon;
        }

        public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon)
        {
            context.Pokemons.Add(pokemon);
            await context.SaveChangesAsync();
            return pokemon;
        }

        public async Task DeletePokemonAsync(int id)
        {
            Pokemon pokemon = await context.Pokemons.FirstOrDefaultAsync(pokemon => pokemon.Id == id);
            context.Remove(pokemon);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            BasePokemon basePokemon = await context.BasePokemons
                .Include(bp => bp.BaseStats)
                .AsNoTracking()
                .FirstOrDefaultAsync(bp => bp.Id == pokemon.BasePokemon.Id);

            pokemon.BasePokemon = basePokemon;

            List<SelectedMove> selectedMoves = await context.SelectedMoves.Where(sm => sm.PokemonId == pokemon.Id).ToListAsync();
            context.SelectedMoves.RemoveRange(selectedMoves);

            context.Update(pokemon);
            await context.SaveChangesAsync();
        }
    }
}
