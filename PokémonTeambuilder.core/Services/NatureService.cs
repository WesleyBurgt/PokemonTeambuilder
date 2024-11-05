using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;
using System.Collections.Generic;

namespace PokémonTeambuilder.core.Services
{
    public class NatureService
    {
        private readonly INatureRepos natureRepos;
        public NatureService(INatureRepos natureRepos)
        {
            this.natureRepos = natureRepos;
        }

        public async Task<List<Nature>> GetAllNatures()
        {
            List<Nature> natures = [];
            try
            {
                natures = await natureRepos.GetAllNaturesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("could not get Natures");
            }

            foreach (Nature nature in natures)
            {
                ValidateNature(nature);
            }
            return natures;
        }

        private void ValidateNature(Nature nature)
        {
            //TODO: make custom Exceptions
            if (nature.Id <= 0)
                throw new Exception("Nature Id cannot be" + nature.Id);
            if (string.IsNullOrEmpty(nature.Name))
                throw new Exception("Nature Name cannot be null or empty");
        }
    }
}
