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

                return Ok("Database updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }

        [HttpPost("UpdateNatures")]
        public async Task<IActionResult> UpdateNatures()
        {
            try
            {
                await natureService.FetchAndSaveNaturesAsync();

                return Ok("Natures updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }

        [HttpPost("UpdateItems")]
        public async Task<IActionResult> UpdateItems()
        {
            try
            {
                await itemService.FetchAndSaveItemsAsync();

                return Ok("Items updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }

        [HttpPost("UpdateAbilities")]
        public async Task<IActionResult> UpdateAbilities()
        {
            try
            {
                await abilityService.FetchAndSaveAbilitiesAsync();

                return Ok("Abilities updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }

        [HttpPost("UpdateTypings")]
        public async Task<IActionResult> UpdateTypings()
        {
            try
            {
                await typingService.FetchAndSaveTypingsAsync();

                return Ok("Typings updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }

        [HttpPost("UpdateMoves")]
        public async Task<IActionResult> UpdateMoves()
        {
            try
            {
                await moveService.FetchAndSaveMovesAsync();

                return Ok("Moves updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }

        [HttpPost("UpdateBasePokemons")]
        public async Task<IActionResult> UpdateBasePokemons()
        {
            try
            {
                await basePokemonService.FetchAndSaveBasePokemonsAsync();

                return Ok("BasePokemons updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"{ex.Message} \n inner expection: {ex.InnerException?.Message}" });
            }
        }
    }
}
