using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using PokémonTeambuilder.DTOs;

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
                List<Nature> Natures = await natureService.GetAllNaturesAsync();
                int count = await natureService.GetNatureCountAsync();
                List<NatureDto> natureDtos = new List<NatureDto>();

                foreach (Nature nature in Natures)
                {
                    natureDtos.Add(MapNatureToDto(nature));
                }

                ApiListResponse response = new ApiListResponse
                {
                    Results = natureDtos,
                    Count = count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private NatureDto MapNatureToDto(Nature nature)
        {
            return new NatureDto
            {
                Id = nature.Id,
                Name = nature.Name,
                Up = nature.Up != null ? char.ToLower(nature.Up.ToString()[0]) + nature.Up.ToString().Substring(1) : null,
                Down = nature.Down != null ? char.ToLower(nature.Down.ToString()[0]) + nature.Down.ToString().Substring(1) : null,
            };
        }

    }
}
