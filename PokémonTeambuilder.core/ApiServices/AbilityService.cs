﻿using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
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

        public async Task GetAllAbilitysAndSaveThem()
        {
            List<Ability> abilitys = await abilityWrapper.GetAllAbilities();
            foreach (Ability ability in abilitys)
            {
                ValidateAbility(ability);
            }
            await abilityRepos.SetAllAbilitiesAsync(abilitys);
        }

        private void ValidateAbility(Ability ability)
        {
            //TODO: make custom Exceptions
            if (ability.Id <= 0)
                throw new Exception("Ability Id cannot be" + ability.Id);
            if (string.IsNullOrEmpty(ability.Name))
                throw new Exception("Ability Name cannot be null or empty");
        }
    }
}
