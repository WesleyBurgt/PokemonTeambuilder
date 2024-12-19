using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using PokémonTeambuilder.DTOs;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoveController : ControllerBase
    {
        private readonly MoveService moveService;

        public MoveController(PokemonTeambuilderDbContext context)
        {
            moveService = new MoveService(new MoveRepos(context));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Move> Moves = await moveService.GetAllMovesAsync();
                int count = await moveService.GetMoveCountAsync();
                List<MoveDto> moveDtos = new List<MoveDto>();

                foreach (Move move in Moves)
                {
                    moveDtos.Add(MapMoveToDto(move));
                }

                ApiListResponse response = new ApiListResponse
                {
                    Results = moveDtos,
                    Count = count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private MoveDto MapMoveToDto(Move move)
        {
            return new MoveDto
            {
                Id = move.Id,
                Name = move.Name,
                Accuracy = move.Accuracy,
                BasePower = move.BasePower,
                Category = move.Category,
                Description = move.Description,
                PP = move.PP,
                Typing = MapTypingToRelationlessDto(move.Typing)
            };
        }

        private TypingRelationlessDto MapTypingToRelationlessDto(Typing typing)
        {
            return new TypingRelationlessDto
            {
                Id = typing.Id,
                Name = typing.Name
            };
        }
    }
}
