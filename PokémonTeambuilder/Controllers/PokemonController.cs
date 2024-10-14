using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.apiwrapper;
using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.DbContext;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : Controller
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }
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
