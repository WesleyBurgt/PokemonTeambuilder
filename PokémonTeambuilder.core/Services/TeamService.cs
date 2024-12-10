using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class TeamService
    {
        private readonly ITeamRepos teamRepos;
        public TeamService(ITeamRepos teamRepos)
        {
            this.teamRepos = teamRepos;
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Team Id cannot be" + id, id);

            Team team = await teamRepos.GetTeamByIdAsync(id);
            ValidateTeam(team);
            return team;
        }

        public async Task<List<Team>> GetTeamsByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidNameException("Username cannot be null or empty", username, typeof(string));

            List<Team> teams = await teamRepos.GetTeamsByUsernameAsync(username);
            foreach(Team team in teams)
            {
                ValidateTeam(team);
            }
            return teams;
        }

        public async Task<int> GetTeamCountByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                    throw new InvalidNameException("Username cannot be null or empty", username, typeof(string));

            int count = await teamRepos.GetTeamCountByUsernameAsync(username);
            return count;
        }

        public async Task<Team> CreateTeamAsync(string username)
        {
            string standardName = "new pokemon team";
            Team team = new Team
            {
                Name = standardName
            };
            await teamRepos.CreateTeamAsync(username, team);
            return team;
        }

        public async Task SetTeamNameAsync(int teamId, string teamName)
        {
            if (teamId <= 0)
                throw new InvalidIdException("Team Id cannot be" + teamId, teamId);
            if (string.IsNullOrEmpty(teamName))
                throw new InvalidNameException("Team Name cannot be null or empty", teamName);

            await teamRepos.SetTeamNameAsync(teamId, teamName);
        }

        public async Task<int> GetPokemonCountAsync(int teamId)
        {
            if (teamId <= 0)
                throw new InvalidIdException("Team Id cannot be" + teamId, teamId);

            int count = await teamRepos.GetPokemonCountAsync(teamId);
            return count;
        }

        public async Task AddPokemonToTeamAsync(int teamId, Pokemon pokemon)
        {
            if (teamId <= 0)
                throw new InvalidIdException("Team Id cannot be" + teamId, teamId);
            if (pokemon.BasePokemon.Id <= 0) 
                throw new InvalidIdException("BasePokemon Id cannot be" + pokemon.BasePokemon.Id, pokemon.BasePokemon.Id, pokemon.BasePokemon.GetType());
            int pokemonCount = await GetPokemonCountAsync(teamId);
            if (pokemonCount >= 6)
                throw new InvalidAmountException("Cannot have more than 6 pokemon on a team", pokemonCount, new Range(0, 5));

            await teamRepos.AddPokemonToTeamAsync(teamId, pokemon);
        }

        public async Task RemovePokemonFromTeamAsync(int teamId, int pokemonId)
        {
            if (teamId <= 0)
                throw new InvalidIdException("Team Id cannot be" + teamId, teamId);
            if (pokemonId <= 0)
                throw new InvalidIdException("Pokemon Id cannot be" + pokemonId, pokemonId);

            await teamRepos.RemovePokemonFromTeamAsync(teamId, pokemonId);
        }

        private void ValidateTeam(Team team)
        {
            if (team.Id <= 0)
                throw new InvalidIdException("Team Id cannot be" + team.Id, team.Id, team.GetType());
            if (string.IsNullOrEmpty(team.Name))
                throw new InvalidNameException("Team Name cannot be null or empty", team.Name, team.GetType());
            if (team.PokemonCount > 6)
                throw new InvalidAmountException("Cannot have more than 6 pokemon on a team", team.PokemonCount, new Range(0, 6));
        }
    }
}
