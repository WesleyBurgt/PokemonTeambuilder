using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiInterfaces
{
    public interface IAbilityWrapper
    {
        Task<List<Ability>> GetAllAbilitiesAsync();
    }
}
