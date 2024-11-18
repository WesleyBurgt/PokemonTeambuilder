using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IUserRepos
    {
        Task<bool> UsernameTakenAsync(string username);
        Task AddUserAsync(User user);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
