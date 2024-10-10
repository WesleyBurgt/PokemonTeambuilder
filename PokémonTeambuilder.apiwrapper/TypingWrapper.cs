using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.Classes;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class TypingWrapper
    {
        static HttpClient client = new HttpClient();

        public TypingWrapper()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<List<Typing>> GetAllTypings()
        {
            HttpResponseMessage response = await client.GetAsync($"type/");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not get response from api"); //TODO: custom exception
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(types =>
            {
                string url = types["url"].ToString();
                return GetIdOutOfUrl(url);

            }).ToList();

            List<Typing> typings = [];
            List<Task<Typing>> tasks = new List<Task<Typing>>();
            foreach (int id in ids)
            {
                tasks.Add(GetTypingById(id));
            }
            Typing[] typingsArray = await Task.WhenAll(tasks);
            typings = typingsArray.ToList();

            if (typings.Count > 0)
            {
                return typings;
            }
            else
            {
                throw new Exception("could not get pokemon list"); //TODO: custom exception
            }
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        public async Task<Typing> GetTypingById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"type/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not get response from api"); //TODO: custom exception
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            JToken damageRelations = jsonObject["damage_relations"];

            JArray weaknesses = (JArray)damageRelations["double_damage_from"];
            JArray resistances = (JArray)damageRelations["half_damage_from"];
            JArray immunities = (JArray)damageRelations["no_damage_from"];

            List<Typing> weaknessList = GetTypingWithoutRelationsOutOfJArray(weaknesses);
            List<Typing> resistanceList = GetTypingWithoutRelationsOutOfJArray(resistances);
            List<Typing> immunityList = GetTypingWithoutRelationsOutOfJArray(immunities);

            Typing typing = new Typing
            {
                Id = (int)jsonObject["id"],
                Name = jsonObject["name"].ToString(),
                Weaknesses = weaknessList,
                Resistances = resistanceList,
                Immunities = immunityList
            };
            return typing;
        }

        private List<Typing> GetTypingWithoutRelationsOutOfJArray(JArray array)
        {
            List<Typing> typings = array.Select(typing =>
            {
                string name = typing["name"].ToString();
                string url = typing["url"].ToString();
                int id = GetIdOutOfUrl(url);
                return new Typing { Id = id, Name = name };

            }).ToList();
            return typings;
        }
    }
}
