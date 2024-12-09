using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IBasePokemonRepos
    {
        Task<List<BasePokemon>> GetBasePokemonListAsync(int offset, int limit);
        Task<int> GetBasePokemonCountAsync();
        Task<BasePokemon> GetBasePokemonByIdAsync(int id);
        Task SetBasePokemonListAsync(List<BasePokemon> basePokemons);
    }
}
