using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.apiwrapper;
using PokémonTeambuilder.core.ApiServices;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using System.Diagnostics;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly AbilityService abilityService;
        private readonly BasePokemonService basePokemonService;
        private readonly ItemService itemService;
        private readonly MoveService moveService;
        private readonly NatureService natureService;
        private readonly TypingService typingService;

        public DatabaseController(PokemonTeambuilderDbContext context)
        {
            abilityService = new AbilityService(new AbilityWrapper(), new AbilityRepos(context));
            basePokemonService = new BasePokemonService(new BasePokemonWrapper(), new BasePokemonRepos(context));
            itemService = new ItemService(new ItemWrapper(), new ItemRepos(context));
            moveService = new MoveService(new MoveWrapper(), new MoveRepos(context));
            natureService = new NatureService(new NatureWrapper(), new NatureRepos(context));
            typingService = new TypingService(new TypingWrapper(), new TypingRepos(context));
        }

        [HttpPost("UpdateDatabase")]
        public async Task<IActionResult> UpdateDatabase()
        {
            try
            {
                await natureService.FetchAndSaveNaturesAsync();
                await itemService.FetchAndSaveItemsAsync();
                await abilityService.FetchAndSaveAbilitiesAsync();
                await typingService.FetchAndSaveTypingsAsync();
                await moveService.FetchAndSaveMovesAsync();
                await basePokemonService.FetchAndSaveBasePokemonsAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
