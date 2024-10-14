using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.Services
{
    public class TeamService
    {
        //TODO: TeamService
        public async Task<Team> GetTeamById(int id)
        {
            Team team = new Team();
            return team;
        }

        public async Task<List<Team>> GetTeaksByUserId(int id)
        {
            List<Team> teams = [];
            return teams;
        }
    }
}
