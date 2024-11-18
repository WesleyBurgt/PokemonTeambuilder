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

        public async Task<List<Team>> GetTeamsByUserIdAsync(int userId)
        {
            if (userId <= 0)
                throw new InvalidIdException("User Id cannot be" + userId, userId);

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
                throw new InvalidIdException("Team Id cannot be" + teamId, teamId);
            if (string.IsNullOrEmpty(teamName))
                throw new InvalidNameException("Team Name cannot be null or empty", teamName);

            await teamRepos.SetTeamNameAsync(teamId, teamName);
        }

        public async Task AddPokemonToTeamAsync(int teamId, Pokemon pokemon)
        {
            if (teamId <= 0)
                throw new InvalidIdException("Team Id cannot be" + teamId, teamId);
            if (pokemon.BasePokemon.Id <= 0) 
                throw new InvalidIdException("BasePokemon Id cannot be" + pokemon.BasePokemon.Id, pokemon.BasePokemon.Id, pokemon.BasePokemon.GetType());

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
        }
    }
}
