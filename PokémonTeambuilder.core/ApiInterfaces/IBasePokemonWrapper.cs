using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface IBasePokemonWrapper
    {
        Task<List<BasePokemon>> GetPokemonListAsync(int offset, int limit);
    }
}
