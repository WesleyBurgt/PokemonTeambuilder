using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using PokémonTeambuilder.DTOs;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService itemService;

        public ItemController(PokemonTeambuilderDbContext context)
        {
            itemService = new ItemService(new ItemRepos(context));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Item> Items = await itemService.GetAllItemsAsync();
                int count = await itemService.GetItemCountAsync();
                List<ItemDto> itemDtos = new List<ItemDto>();

                foreach (Item item in Items)
                {
                    itemDtos.Add(MapItemToDto(item));
                }

                ApiListResponse response = new ApiListResponse
                {
                    Results = itemDtos,
                    Count = count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private ItemDto MapItemToDto(Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Image = item.Image
            };
        }
    }
}
