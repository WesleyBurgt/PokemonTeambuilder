using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.DbInterfaces
{
    public interface IAbilityRepos
    {
        Task<List<Ability>> GetAllAbilities();
        void SetAllAbilities(List<Ability> ability);
    }
}
