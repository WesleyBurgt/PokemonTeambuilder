using Newtonsoft.Json.Linq;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Exceptions;
using PokémonTeambuilder.core.Models;
using System.Net.Http.Headers;

namespace PokémonTeambuilder.apiwrapper
{
    public class ItemWrapper : IItemWrapper
    {

        static HttpClient client = new HttpClient();

        public ItemWrapper()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            int ItemCount = await GetItemCountAsync();

            HttpResponseMessage response = await client.GetAsync($"item?offset=0&limit={ItemCount}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            JArray jsonArray = (JArray)jsonObject["results"];
            List<int> ids = jsonArray.Select(item =>
            {
                string url = item["url"].ToString();
                return GetIdOutOfUrl(url);

            }).ToList();

            List<Item> items = [];
            List<Task<Item>> tasks = new List<Task<Item>>();
            foreach (int id in ids)
            {
                tasks.Add(GetItem(id));
            }
            Item[] itemArray = await Task.WhenAll(tasks);
            items = itemArray.ToList();

            if (items.Count > 0)
            {
                return items;
            }
            else
            {
                throw new ApiResponseException("could not get item list");
            }
        }

        private async Task<int> GetItemCountAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"item?offset=0&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            int itemCount = (int)jsonObject["count"];
            return itemCount;
        }

        private int GetIdOutOfUrl(string url)
        {
            string[] urlParts = url.Split('/');
            return int.Parse(urlParts[^2]);
        }

        private async Task<Item> GetItem(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"item/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiResponseException("Could not get response from api");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);

            string? description = jsonObject["effect_entries"]?
                .FirstOrDefault(entry => entry["language"]?["name"]?.ToString() == "en")?["short_effect"]?.ToString();

            string? image = jsonObject["sprites"]?["default"]?.Type == JTokenType.Null
                ? null
                : jsonObject["sprites"]?["default"]?.ToString();


            Item item = new Item
            {
                Id = id,
                Name = jsonObject["name"].ToString(),
                Description = description,
                Image = image
            };

            return item;
        }
    }
}
