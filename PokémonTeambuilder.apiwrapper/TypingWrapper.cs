using Newtonsoft.Json.Linq;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using PokémonTeambuilder.core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PokémonTeambuilder.apiwrapper
{
    public class TypingWrapper
    {
        TypeApi api;

        public TypingWrapper()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://pokeapi.co";
            api = new TypeApi(config);
        }

        public async Task<List<Typing>> GetAllTypings()
        {
            var json = api.TypeList();
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
            string json = await api.TypeReadAsync(id);
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
