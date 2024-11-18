using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Enums;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class NatureWrapper : INatureWrapper
    {
        static HttpClient client = new HttpClient();

        public NatureWrapper()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<List<Nature>> GetAllNaturesAsync()
        {
            int NatureCount = await GetNatureCountAsync();

            HttpResponseMessage response = await client.GetAsync($"nature?offset=0&limit={NatureCount}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
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
                throw new ApiResponseException("could not get nature list");
            }
        }

        private async Task<int> GetNatureCountAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"nature?offset=0&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
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
            throw new ApiResponseException("could not convert from string to statsEnum");
        }

        private async Task<Nature> GetNature(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"nature/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
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
