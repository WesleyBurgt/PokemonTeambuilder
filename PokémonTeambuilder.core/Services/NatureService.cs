using PokémonTeambuilder.core.DbInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.core.Services
{
    public class NatureService
    {
        private readonly INatureRepos natureRepos;
        public NatureService(INatureRepos natureRepos)
        {
            this.natureRepos = natureRepos;
        }

        public async Task<List<Nature>> GetAllNaturesAsync()
        {
            List<Nature> natures = [];
            try
            {
                natures = await natureRepos.GetAllNaturesAsync();
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Natures", ex);
            }

            foreach (Nature nature in natures)
            {
                ValidateNature(nature);
            }
            return natures;
        }

        public async Task<int> GetNatureCountAsync()
        {
            try
            {
                int result = await natureRepos.GetNatureCountAsync();
                return result;
            }
            catch (ReposResponseException ex)
            {
                throw new ReposResponseException("Could not get Nature count", ex);
            }
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
