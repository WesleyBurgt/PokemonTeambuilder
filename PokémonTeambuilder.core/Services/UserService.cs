﻿using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class UserService
    {
        private readonly IUserRepos userRepos;

        public UserService(IUserRepos userRepos)
        {
            this.userRepos = userRepos;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool ValidatePassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }

        public async Task<User> RegisterUserAsync(string username, string password)
        {
            if (await userRepos.UsernameTakenAsync(username))
            {
                throw new InvalidNameException("Username is already taken.", username);
            }

            string hashedPassword = HashPassword(password);

            User user = new User
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            await userRepos.AddUserAsync(user);
            return user;
        }

        public async Task<User> AuthenticateUserAsync(string username, string enteredPassword)
        {
            User? user = await userRepos.GetUserByUsernameAsync(username);

            if (user == null || !ValidatePassword(enteredPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password.");

            return user;
        }
    }
}
