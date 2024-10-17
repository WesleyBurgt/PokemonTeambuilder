using PokémonTeambuilder.core.Dto;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IBasePokemonRepos
    {
        Task<List<BasePokemonDto>> GetBasePokemonList(int offset, int limit);
    }
}
