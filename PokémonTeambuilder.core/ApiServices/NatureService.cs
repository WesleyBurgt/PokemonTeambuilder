using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
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
            if (nature.Id <= 0)
                throw new InvalidIdException("Nature Id cannot be" + nature.Id, nature.Id, nature.GetType());
            if (string.IsNullOrEmpty(nature.Name))
                throw new InvalidNameException("Nature Name cannot be null or empty", nature.Name, nature.GetType());
        }
    }
}
