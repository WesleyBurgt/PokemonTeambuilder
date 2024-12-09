using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class TeamRepos : ITeamRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public TeamRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            Team team = await context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            return team;
        }

        public async Task<List<Team>> GetTeamsByUsernameAsync(string username)
        {
            User user = await context.Users.FirstOrDefaultAsync(t => t.Username == username);
            List<Team> teams = await context.Teams.Where(t => t.UserId == user.Id).ToListAsync();
            return teams;
        }

        public async Task<int> GetTeamCountByUsernameAsync(string username)
        {
            User user = await context.Users.FirstOrDefaultAsync(t => t.Username == username);
            int count = await context.Teams.Where(t => t.UserId == user.Id).CountAsync();
            return count;
        }

        public async Task SetTeamNameAsync(int teamId, string teamName)
        {
            Team team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            team.Name = teamName;
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }

        public async Task AddPokemonToTeamAsync(int teamId, Pokemon pokemon)
        {
            Team team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            team.Pokemons.Add(pokemon);
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }

        public async Task RemovePokemonFromTeamAsync(int teamId, int pokemonId)
        {
            Team team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            Pokemon pokemon = team.Pokemons.FirstOrDefault(p => p.Id == pokemonId);
            team.Pokemons.Remove(pokemon);
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }
    }
}
