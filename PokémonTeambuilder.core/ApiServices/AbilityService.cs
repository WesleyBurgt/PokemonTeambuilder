using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiServices
{
    public class AbilityService
    {
        private readonly IAbilityWrapper abilityWrapper;
        private readonly IAbilityRepos abilityRepos;

        public AbilityService(IAbilityWrapper abilityWrapper, IAbilityRepos abilityRepos)
        {
            this.abilityWrapper = abilityWrapper;
            this.abilityRepos = abilityRepos;
        }

        public async Task FetchAndSaveAbilitiesAsync()
        {
            List<Ability> abilitys = await abilityWrapper.GetAllAbilitiesAsync();
            foreach (Ability ability in abilitys)
            {
                ValidateAbility(ability);
            }
            await abilityRepos.SetAllAbilitiesAsync(abilitys);
        }

        private void ValidateAbility(Ability ability)
        {
            if (ability.Id <= 0)
                throw new InvalidIdException("Ability Id cannot be" + ability.Id, ability.Id, ability.GetType());
            if (string.IsNullOrEmpty(ability.Name))
                throw new InvalidNameException("Ability Name cannot be null or empty", ability.Name, ability.GetType());
        }
    }
}
