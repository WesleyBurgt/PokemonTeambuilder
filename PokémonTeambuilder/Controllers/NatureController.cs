using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.apiwrapper;
using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.core.Services;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NatureController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            NatureService natureService = new NatureService(new NatureWrapper());
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
