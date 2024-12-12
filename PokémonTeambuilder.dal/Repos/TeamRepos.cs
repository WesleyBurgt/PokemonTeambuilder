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
                        .ThenInclude(m => m.Typing)
                            .ThenInclude(t => t.Relationships)
                                .ThenInclude(tr => tr.RelatedTyping)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.EVs)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.IVs)
                .FirstOrDefaultAsync(t => t.Id == id);
            return team;
        }

        public async Task<List<Team>> GetTeamsByUsernameAsync(string username)
        {
            User user = await context.Users.FirstOrDefaultAsync(t => t.Username == username);

            if (user == null)
            {
                return new List<Team>();
            }

            List<Team> teams = await context.Teams
                .Where(t => t.UserId == user.Id)
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
                .Include(t =>t.Pokemons)
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
                        .ThenInclude(m => m.Typing)
                            .ThenInclude(t => t.Relationships)
                                .ThenInclude(tr => tr.RelatedTyping)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.EVs)
                .Include(t => t.Pokemons)
                    .ThenInclude(p => p.IVs)
                .ToListAsync();

            return teams;
        }

        public async Task<int> GetTeamCountByUsernameAsync(string username)
        {
            User user = await context.Users.FirstOrDefaultAsync(t => t.Username == username);
            int count = await context.Teams.Where(t => t.UserId == user.Id).CountAsync();
            return count;
        }

        public async Task CreateTeamAsync(string username, Team team)
        {
            User user = await context.Users.FirstOrDefaultAsync(t => t.Username == username);
            team.UserId = user.Id;
            context.Teams.Add(team);
            await context.SaveChangesAsync();
        }

        public async Task SetTeamNameAsync(int teamId, string teamName)
        {
            Team team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            team.Name = teamName;
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetPokemonCountAsync(int teamId)
        {
            Team team = await context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            int count = team.PokemonCount;
            return count;
        }

        public async Task AddPokemonToTeamAsync(int teamId, Pokemon pokemon)
        {
            Team team = await context.Teams
                .Include(t => t.Pokemons)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
                throw new Exception("Team not found");
            if (team.Pokemons == null)
                team.Pokemons = new List<Pokemon>();

            team.Pokemons.Add(pokemon);
            team.PokemonCount = team.Pokemons.Count;
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }

        public async Task RemovePokemonFromTeamAsync(int teamId, int pokemonId)
        {
            Team team = await context.Teams
                .Include(t => t.Pokemons)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
                throw new Exception("Team not found");
            if (team.Pokemons == null)
                team.Pokemons = new List<Pokemon>();

            Pokemon pokemon = team.Pokemons.FirstOrDefault(p => p.Id == pokemonId);
            team.Pokemons.Remove(pokemon);
            team.PokemonCount = team.Pokemons.Count;
            context.Teams.Update(team);
            await context.SaveChangesAsync();
        }
    }
}
