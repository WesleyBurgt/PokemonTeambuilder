using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using PokémonTeambuilder.apiwrapper;
using PokémonTeambuilder.core;
using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.core.ApiInterfaces;
using PokémonTeambuilder.core.Services;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(int offset, int limit)
        {
            BasePokemonService basePokemonService = new BasePokemonService(new BasePokemonWrapper());
            try
            {
                List<BasePokemon> list = await basePokemonService.GetPokemonList(offset, limit);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
