using Newtonsoft.Json.Linq;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.apiwrapper
{
    public class BasePokemonWrapper : IBasePokemonWrapper
    {
        public async Task<List<BasePokemon>> GetPokemonList(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new Exception("offset must be 0 or more"); //TODO: custom exception
            }
            Configuration config = new Configuration();
            config.BasePath = "https://pokeapi.co";
            PokemonApi api = new PokemonApi(config);

            var json = api.PokemonList(limit, offset);
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(pokemon =>
            {
                string url = (string)pokemon["url"];
                return GetIdOutOfUrl(url);

            }).ToList();

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
                throw new Exception("could not get pokemon list"); //TODO: custom exception
            }
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        private async Task<BasePokemon> GetBasePokemon(int id)
        {
            Configuration config = new Configuration();
            config.BasePath = "https://pokeapi.co";
            PokemonApi api = new PokemonApi(config);

            string json = await api.PokemonReadAsync(id);
            JObject jsonObject = JObject.Parse(json);

            BasePokemon pokemon = new BasePokemon
            {
                Id = (int)jsonObject["id"],
                Name = jsonObject["name"].ToString(),
                Sprite = jsonObject["sprites"]["front_default"].ToString()
            };

            List<Typing> types = new List<Typing>();
            foreach (var type in jsonObject["types"])
            {
                JToken typeToken = type["type"];
                string typeName = typeToken["name"].ToString();
                int typeId = GetIdOutOfUrl(typeToken["url"].ToString());
                types.Add(new Typing { Name = typeName, Id = typeId });
            }
            pokemon.Typings = types;

            Stats stats = new Stats();
            foreach (var stat in jsonObject["stats"])
            {
                string statName = (string)stat["stat"]["name"];
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

            List<Ability> abilities = new List<Ability>();
            foreach (var ability in jsonObject["abilities"])
            {
                JToken abilityToken = ability["ability"];
                string abilityName = abilityToken["name"].ToString();
                int abilityId = GetIdOutOfUrl(abilityToken["url"].ToString());
                abilities.Add(new Ability { Name = abilityName, Id = abilityId });
            }
            pokemon.Abilities = abilities;

            return pokemon;
        }
    }
}
