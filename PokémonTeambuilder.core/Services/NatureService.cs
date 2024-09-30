using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.Services
{
    public class NatureService
    {
        private readonly INatureWrapper natureWrapper;
        public NatureService(INatureWrapper natureWrapper)
        {
            this.natureWrapper = natureWrapper;
        }

        public async Task<List<Nature>> GetAllNatures()
        {
            List<Nature> list = await natureWrapper.GetAllNatures();
            foreach (Nature nature in list)
            {
                //TODO: make custom Exceptions
                if (nature.Id <= 0)
                    throw new Exception("Nature Id cannot be" + nature.Id);
                if (String.IsNullOrEmpty(nature.Name))
                    throw new Exception("Nature Name cannot be null or empty");
            }
            return list;
        }
    }
}
