using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.core.DalInterfaces;

namespace PokémonTeambuilder.core.Services
{
    public class TeamService
    {
        private readonly ITeamDAL teamDAL;

        public TeamService(ITeamDAL teamDAL)
        {
            this.teamDAL = teamDAL;
        }

        public async Task<Team> GetTeamById(int id)
        {
            Team team = await teamDAL.GetTeamById(id);
            //TODO: check for exceptions
            return team;
        }

        public async Task<List<Team>> GetTeaksByUserId(int id)
        {
            List<Team> teams = await teamDAL.GetTeamsByUserId(id);
            //TODO: check for exceptions
            return teams;
        }
    }
}
