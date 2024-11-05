using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.ApiServices
{
    public class NatureService
    {
        private readonly INatureWrapper natureWrapper;
        private readonly INatureRepos natureRepos;

        public NatureService(INatureWrapper natureWrapper, INatureRepos natureRepos)
        {
            this.natureWrapper = natureWrapper;
            this.natureRepos = natureRepos;
        }

        public async Task FetchAndSaveNaturesAsync()
        {
            List<Nature> natures = await natureWrapper.GetAllNaturesAsync();
            foreach (Nature nature in natures)
            {
                ValidateNature(nature);
            }
            await natureRepos.SetAllNaturesAsync(natures);
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
