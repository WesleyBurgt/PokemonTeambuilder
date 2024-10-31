using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IBasePokemonRepos
    {
        Task<List<BasePokemon>> GetBasePokemonList(int offset, int limit);
        void SetBasePokemonList(List<BasePokemon> basePokemons);
    }
}
