using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Models;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class AbilityWrapper : IAbilityWrapper
    {
        static HttpClient client = new HttpClient();

        public AbilityWrapper()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<List<Ability>> GetAllAbilities()
        {
            int AbilityCount = await GetAbilityCount();

            HttpResponseMessage response = await client.GetAsync($"ability?offset=0&limit={AbilityCount}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not get response from api"); //TODO: custom exception
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(ability =>
            {
                string url = ability["url"].ToString();
                return GetIdOutOfUrl(url);

            }).ToList();

            List<Ability> abilities = [];
            List<Task<Ability>> tasks = new List<Task<Ability>>();
            foreach (int id in ids)
            {
                tasks.Add(GetAbility(id));
            }
            Ability[] abilityArray = await Task.WhenAll(tasks);
            abilities = abilityArray.ToList();

            if (abilities.Count > 0)
            {
                return abilities;
            }
            else
            {
                throw new Exception("could not get ability list"); //TODO: custom exception
            }

        }

        private async Task<int> GetAbilityCount()
        {
            HttpResponseMessage response = await client.GetAsync($"ability?offset=0&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not get response from api"); //TODO: custom exception
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            int abilityCount = int.Parse(jsonObject["count"].ToString());
            return abilityCount;
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        private async Task<Ability> GetAbility(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"ability/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not get response from api"); //TODO: custom exception
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);

            string description = jsonObject["effect_entries"]?
                .FirstOrDefault(entry => entry["language"]?["name"]?.ToString() == "en")?["short_effect"]?.ToString();

            Ability ability = new Ability
            {
                Id = id,
                Name = jsonObject["name"].ToString(),
                Description = description
            };

            return ability;
        }
    }
}
