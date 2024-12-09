﻿using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface ITeamRepos
    {
        Task<Team> GetTeamByIdAsync(int id);
        Task<List<Team>> GetTeamsByUsernameAsync(string username);
        Task<int> GetTeamCountByUsernameAsync(string username);
        Task SetTeamNameAsync(int teamId, string teamName);
        Task AddPokemonToTeamAsync(int teamId, Pokemon pokemon);
        Task RemovePokemonFromTeamAsync(int teamId, int pokemonId);
    }
}
