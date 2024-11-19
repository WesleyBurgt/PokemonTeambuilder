using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class BasePokemonWrapper : IBasePokemonWrapper
    {
        static HttpClient client = new HttpClient();

        private TypingWrapper typingWrapper = new TypingWrapper();
        private List<Typing> typings = [];

        public BasePokemonWrapper()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<int> GetPokemonCountAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"pokemon?offset=0&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            int pokemonCount = (int)jsonObject["count"];
            return pokemonCount;
        }

        public async Task<List<BasePokemon>> GetAllBasePokemonsAsync()
        {
            int pokemonCount = await GetPokemonCountAsync();

            HttpResponseMessage response = await client.GetAsync($"pokemon?offset=0&limit={pokemonCount}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }
            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(pokemon =>
            {
                string url = pokemon["url"].ToString();
                return GetIdOutOfUrl(url);

            }).ToList();

            typings = await typingWrapper.GetAllTypingsAsync();
            List<BasePokemon> pokemons = [];
            List<Task<BasePokemon>> tasks = new List<Task<BasePokemon>>();
            foreach (int id in ids)
            {
                tasks.Add(GetBasePokemon(id));
            }
            BasePokemon[] pokemonsArray = await Task.WhenAll(tasks);
            pokemons = pokemonsArray.ToList();

            if (pokemons.Count > 0)
            {
                return pokemons;
            }
            else
            {
                throw new ApiResponseException("could not get pokemon list");
            }
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        private async Task<BasePokemon> GetBasePokemon(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"pokemon/{id}/");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);

            BasePokemon pokemon = new BasePokemon
            {
                Id = (int)jsonObject["id"],
                Name = jsonObject["name"].ToString(),
                Sprite = jsonObject["sprites"]["front_default"].ToString()
            };

            List<BasePokemonTyping> basePokemonTypings = new List<BasePokemonTyping>();
            foreach (var type in jsonObject["types"])
            {
                JToken typeToken = type["type"];
                string typeName = typeToken["name"].ToString();
                int slot = int.Parse(type["slot"].ToString());
                Typing? typing = typings.FirstOrDefault(t => t.Name == typeName);
                if (typing != null)
                {
                    basePokemonTypings.Add(new BasePokemonTyping { BasePokemon = pokemon, BasePokemonId = id, Slot = slot, Typing = typing, TypingId = typing.Id });
                }
            }
            pokemon.Typings = basePokemonTypings;

            Stats stats = new Stats();
            foreach (var stat in jsonObject["stats"])
            {
                string statName = stat["stat"]["name"].ToString();
                int baseStat = (int)stat["base_stat"];
                switch (statName)
                {
                    case "hp": stats.Hp = baseStat; break;
                    case "attack": stats.Attack = baseStat; break;
                    case "defense": stats.Defense = baseStat; break;
                    case "special-attack": stats.SpecialAttack = baseStat; break;
                    case "special-defense": stats.SpecialDefense = baseStat; break;
                    case "speed": stats.Speed = baseStat; break;
                };
            }
            pokemon.BaseStats = stats;

            List<Move> moves = new List<Move>();
            foreach (var move in jsonObject["moves"])
            {
                JToken moveToken = move["move"];
                string moveName = moveToken["name"].ToString();
                int moveId = GetIdOutOfUrl(moveToken["url"].ToString());
                moves.Add(new Move { Name = moveName, Id = moveId });
            }
            pokemon.Moves = moves;

            List<BasePokemonAbility> abilities = new List<BasePokemonAbility>();
            foreach (var ability in jsonObject["abilities"])
            {
                JToken abilityToken = ability["ability"];
                string abilityName = abilityToken["name"].ToString();
                int abilityId = GetIdOutOfUrl(abilityToken["url"].ToString());
                bool isHidden = Boolean.Parse(ability["is_hidden"].ToString());
                int slot = int.Parse(ability["slot"].ToString());
                abilities.Add(new BasePokemonAbility { Ability = new Ability { Name = abilityName, Id = abilityId }, AbilityId = abilityId, BasePokemon = pokemon, BasePokemonId = pokemon.Id, IsHidden = isHidden, Slot = slot });
            }
            pokemon.Abilities = abilities;

            return pokemon;
        }
    }
}
