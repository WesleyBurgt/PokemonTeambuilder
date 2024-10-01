using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.DalInterfaces
{
    public interface ITeamDAL
    {
        public Task<Team> GetTeamById(int id);
        public Task<List<Team>> GetTeamsByUserId(int id);
    }
}
