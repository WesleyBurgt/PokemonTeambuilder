using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly BasePokemonService basePokemonService;

        public PokemonController(PokemonTeambuilderDbContext context)
        {
            basePokemonService = new BasePokemonService(new BasePokemonRepos(context));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int offset, int limit)
        {
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
