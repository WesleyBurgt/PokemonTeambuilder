using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Enums;
using PokémonTeambuilder.core.Models;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class TypingWrapper : ITypingWrapper
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

        public async Task<List<Typing>> GetAllTypingsAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"type");
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
                throw new Exception("could not get type list"); //TODO: custom exception
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

            Typing typing = new Typing
            {
                Id = (int)jsonObject["id"],
                Name = jsonObject["name"].ToString(),
            };
            typing.Relationships = GetTypingRelationships(typing, damageRelations);

            return typing;
        }

        private List<TypingRelationship> GetTypingRelationships(Typing typing, JToken typingRelations)
        {
            JArray weaknesses = (JArray)typingRelations["double_damage_from"];
            JArray resistances = (JArray)typingRelations["half_damage_from"];
            JArray immunities = (JArray)typingRelations["no_damage_from"];

            List<Typing> weaknessList = GetTypingWithoutRelationsOutOfJArray(weaknesses);
            List<Typing> resistanceList = GetTypingWithoutRelationsOutOfJArray(resistances);
            List<Typing> immunityList = GetTypingWithoutRelationsOutOfJArray(immunities);

            List<TypingRelationship> weaknessRelationships = TypingListToTypingRelationshipList(typing, weaknessList, TypingRelation.weak);
            List<TypingRelationship> resistanceRelationships = TypingListToTypingRelationshipList(typing, resistanceList, TypingRelation.resist);
            List<TypingRelationship> immunityRelationships = TypingListToTypingRelationshipList(typing, immunityList, TypingRelation.immune);

            List<TypingRelationship> result = [];
            result.AddRange(weaknessRelationships);
            result.AddRange(resistanceRelationships);
            result.AddRange(immunityRelationships);
            return result;
        }

        private List<TypingRelationship> TypingListToTypingRelationshipList(Typing typing, List<Typing> typingList, TypingRelation relation)
        {
            List<TypingRelationship> result = [];
            foreach (Typing relatedTyping in typingList)
            {
                result.Add(new TypingRelationship
                {
                    Typing = typing,
                    TypingId = typing.Id,
                    RelatedTyping = relatedTyping,
                    RelatedTypingId = relatedTyping.Id,
                    Relation = relation
                });
            }
            return result;
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
