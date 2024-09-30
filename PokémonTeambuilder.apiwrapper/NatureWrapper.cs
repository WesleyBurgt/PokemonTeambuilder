using Newtonsoft.Json.Linq;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.apiwrapper
{
    public class NatureWrapper : INatureWrapper
    {
        public async Task<List<Nature>> GetAllNatures()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://pokeapi.co";
            NatureApi api = new NatureApi(config);

            var json = api.NatureList(GetNatureCount(api), 0);
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(nature =>
            {
                string url = nature["url"].ToString();
                return GetIdOutOfUrl(url);

            }).ToList();

            List<Nature> natures = [];
            List<Task<Nature>> tasks = new List<Task<Nature>>();
            foreach (int id in ids)
            {
                tasks.Add(GetNature(id));
            }
            Nature[] natureArray = await Task.WhenAll(tasks);
            natures = natureArray.ToList();

            if (natures.Count > 0)
            {
                return natures;
            }
            else
            {
                throw new Exception("could not get nature list"); //TODO: custom exception
            }
        }

        private int GetNatureCount(NatureApi api)
        {
            var json = api.NatureList(1, 0);
            JObject jsonObject = JObject.Parse(json);
            int natureCount = (int)jsonObject["count"];
            return natureCount;
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        private StatsEnum? StringToStatsEnum(string statName)
        {
            switch (statName)
            {
                case "hp": return StatsEnum.Hp;
                case "attack": return StatsEnum.Attack;
                case "defense": return StatsEnum.Defense;
                case "special-attack": return StatsEnum.SpecialAttack;
                case "special-defense": return StatsEnum.SpecialDefense;
                case "speed": return StatsEnum.Speed;
                case "": return null;
            };
            throw new Exception("could not convert from string to statsEnum"); //TODO: custom exception
        }

        private async Task<Nature> GetNature(int id)
        {
            Configuration config = new Configuration();
            config.BasePath = "https://pokeapi.co";
            NatureApi api = new NatureApi(config);

            string json = await api.NatureReadAsync(id);
            JObject jsonObject = JObject.Parse(json);

            JToken jsonIncreasedStat = jsonObject["increased_stat"];
            JToken jsonDecreasedStat = jsonObject["decreased_stat"];

            Nature nature = new Nature
            {
                Id = id,
                Name = jsonObject["name"].ToString(),
            };

            if (jsonIncreasedStat.HasValues)
            {
                nature.Up = StringToStatsEnum(jsonIncreasedStat["name"].ToString());
            }
            if (jsonDecreasedStat.HasValues)
            {
                nature.Down = StringToStatsEnum(jsonDecreasedStat["name"].ToString());
            }

            return nature;
        }
    }
}
