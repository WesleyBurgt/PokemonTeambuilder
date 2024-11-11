using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.DTOs;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<string> Genders = new List<string>();
                int count = Enum.GetValues(typeof(Gender)).Length;

                foreach (Gender gender in Enum.GetValues(typeof(Gender)))
                {
                    Genders.Add(gender.ToString());
                }

                ApiListResponse response = new ApiListResponse
                {
                    Results = Genders,
                    Count = count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
