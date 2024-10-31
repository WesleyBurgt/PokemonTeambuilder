using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.apiwrapper;
using PokémonTeambuilder.core.ApiServices;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly BasePokemonService basePokemonService;
        private readonly NatureService natureService;
        private readonly TypingService typingService;

        public DatabaseController(PokemonTeambuilderDbContext context)
        {
            basePokemonService = new BasePokemonService(new BasePokemonWrapper(), new BasePokemonRepos(context));
            natureService = new NatureService(new NatureWrapper(), new NatureRepos(context));
            typingService = new TypingService(new TypingWrapper(), new TypingRepos(context));
        }

        [HttpPost("UpdateDatabase")]
        public async Task<IActionResult> UpdateDatabase()
        {
            try
            {
                await natureService.GetAllNaturesAndSaveThem();
                await typingService.GetAllTypingsAndSaveThem();
                await basePokemonService.GetAllBasePokemonAndSaveThem();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
