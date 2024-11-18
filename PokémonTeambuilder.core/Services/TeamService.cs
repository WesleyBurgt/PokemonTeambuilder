using PokémonTeambuilder.core.DbInterfaces;
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
                throw new Exception("Team Id cannot be" + id);

            Team team = await teamRepos.GetTeamByIdAsync(id);
            ValidateTeam(team);
            return team;
        }

        public async Task<List<Team>> GetTeamsByUserIdAsync(int userId)
        {
            if (userId <= 0)
                throw new Exception("User Id cannot be" + userId);

            List<Team> teams = await teamRepos.GetTeamsByUserIdAsync(userId);
            foreach(Team team in teams)
            {
                ValidateTeam(team);
            }
            return teams;
        }

        public async Task SetTeamNameAsync(int teamId, string teamName)
        {
            if (teamId <= 0)
                throw new Exception("Team Id cannot be" + teamId);
            if (string.IsNullOrEmpty(teamName))
                throw new Exception("Team Name cannot be null or empty");

            await teamRepos.SetTeamNameAsync(teamId, teamName);
        }

        public async Task AddPokemonToTeamAsync(int teamId, Pokemon pokemon)
        {
            if (teamId <= 0)
                throw new Exception("Team Id cannot be" + teamId);
            if (pokemon.BasePokemon.Id <= 0) 
                throw new Exception("BasePokemon Id cannot be" + pokemon.BasePokemon.Id);

            await teamRepos.AddPokemonToTeamAsync(teamId, pokemon);
        }

        public async Task RemovePokemonFromTeamAsync(int teamId, int pokemonId)
        {
            if (teamId <= 0)
                throw new Exception("Team Id cannot be" + teamId);
            if (pokemonId <= 0)
                throw new Exception("Pokemon Id cannot be" + pokemonId);

            await teamRepos.RemovePokemonFromTeamAsync(teamId, pokemonId);
        }

        private void ValidateTeam(Team team)
        {
            //TODO: make custom Exceptions
            if (team.Id <= 0)
                throw new Exception("Team Id cannot be" + team.Id);
            if (string.IsNullOrEmpty(team.Name))
                throw new Exception("Team Name cannot be null or empty");
        }
    }
}
