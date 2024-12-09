using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IPokemonRepos
    {
        Task<List<Pokemon>> GetPokemonsByTeamIdAsync(int teamId);
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<Pokemon> CreatePokemonAsync(Pokemon pokemon);
        Task DeletePokemonAsync(int id);
        Task UpdatePokemonAsync(Pokemon pokemon);
    }
}
