using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NatureController : ControllerBase
    {
        private readonly NatureService natureService;

        public NatureController(PokemonTeambuilderDbContext context)
        {
            natureService = new NatureService(new NatureRepos(context));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Nature> list = await natureService.GetAllNatures();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
