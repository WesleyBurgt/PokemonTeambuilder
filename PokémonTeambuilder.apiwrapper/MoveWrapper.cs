using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class MoveWrapper : IMoveWrapper
    {
        static HttpClient client = new HttpClient();

        public MoveWrapper()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<List<Move>> GetAllMovesAsync()
        {
            int MoveCount = await GetMoveCountAsync();

            HttpResponseMessage response = await client.GetAsync($"move?offset=0&limit={MoveCount}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(move =>
            {
                string url = move["url"].ToString();
                return GetIdOutOfUrl(url);

            }).ToList();

            List<Move> moves = [];
            List<Task<Move>> tasks = new List<Task<Move>>();
            foreach (int id in ids)
            {
                tasks.Add(GetMove(id));
            }
            Move[] moveArray = await Task.WhenAll(tasks);
            moves = moveArray.ToList();

            if (moves.Count > 0)
            {
                return moves;
            }
            else
            {
                throw new ApiResponseException("could not get move list");
            }
        }

        private async Task<int> GetMoveCountAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"move?offset=0&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            int moveCount = (int)jsonObject["count"];
            return moveCount;
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        private async Task<Move> GetMove(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"move/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);

            string category = jsonObject["damage_class"]?["name"]?.ToString();

            int? accuracy = null;
            JToken accuracyToken = jsonObject["accuracy"];
            if (accuracyToken != null && accuracyToken.Type != JTokenType.Null)
            {
                accuracy = int.Parse(accuracyToken.ToString());
            }

            int? basePower = null;
            JToken powerToken = jsonObject["power"];
            if (powerToken != null && powerToken.Type != JTokenType.Null)
            {
                basePower = int.Parse(powerToken.ToString());
            }

            int? pp = null;
            JToken ppToken = jsonObject["pp"];
            if (ppToken != null && ppToken.Type != JTokenType.Null)
            {
                pp = int.Parse(ppToken.ToString());
            }

            string description = jsonObject["effect_entries"]?
                .FirstOrDefault(entry => entry["language"]?["name"]?.ToString() == "en")?["short_effect"]?.ToString();

            Typing typing = new Typing();
            JToken typingToken = jsonObject["type"];
            if (typingToken != null)
            {
                typing = new Typing
                {
                    Id = GetIdOutOfUrl(typingToken["url"].ToString()),
                    Name = typingToken["name"].ToString()
                };
            }

            Move move = new Move
            {
                Id = id,
                Name = jsonObject["name"].ToString(),
                Category = category,
                Accuracy = accuracy,
                BasePower = basePower,
                PP = pp,
                Description = description,
                Typing = typing
            };

            return move;
        }
    }
}
