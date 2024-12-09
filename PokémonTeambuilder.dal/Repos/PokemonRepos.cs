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
            Team team = await context.Teams.FirstOrDefaultAsync(team => team.Id == teamId);
            List<Pokemon> pokemons = team.Pokemons.ToList();
            return pokemons;
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            Pokemon pokemon = await context.Pokemons.FirstOrDefaultAsync(pokemon => pokemon.Id == id);
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
            context.Update(pokemon);
            await context.SaveChangesAsync();
        }
    }
}
