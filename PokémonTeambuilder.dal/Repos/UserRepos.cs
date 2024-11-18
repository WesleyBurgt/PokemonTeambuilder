using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder.dal.Repos
{
    public class UserRepos : IUserRepos
    {
        private readonly PokemonTeambuilderDbContext context;

        public UserRepos(PokemonTeambuilderDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> UsernameTakenAsync(string username)
        {
            bool result = await context.Users.AnyAsync(u => u.Username == username);
            return result;
        }

        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }
    }
}
