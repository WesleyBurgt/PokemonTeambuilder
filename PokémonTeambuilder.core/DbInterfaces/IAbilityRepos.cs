using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IAbilityRepos
    {
        Task<List<Ability>> GetAllAbilitiesAsync();
        Task SetAllAbilitiesAsync(List<Ability> ability);
    }
}
