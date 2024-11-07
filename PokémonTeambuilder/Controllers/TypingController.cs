using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Enums;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using PokémonTeambuilder.DTOs;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypingController : ControllerBase
    {

        private readonly TypingService typingService;

        public TypingController(PokemonTeambuilderDbContext context)
        {
            typingService = new TypingService(new TypingRepos(context));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Typing> Typings = await typingService.GetAllTypingsAsync();
                int count = await typingService.GetTypingCountAsync();
                List<TypingDto> typingDtos = new List<TypingDto>();

                foreach (Typing typing in Typings)
                {
                    typingDtos.Add(MapTypingToDto(typing));
                }

                ApiListResponse response = new ApiListResponse
                {
                    Results = typingDtos,
                    Count = count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private TypingDto MapTypingToDto(Typing typing)
        {
            return new TypingDto
            {
                Id = typing.Id,
                Name = typing.Name,
                Weaknesses = typing.Relationships
                    .Where(r => r.Relation == TypingRelation.weak)
                    .Select(r => new TypingRelationlessDto { Id = r.RelatedTyping.Id, Name = r.RelatedTyping.Name })
                    .ToList(),
                Resistances = typing.Relationships
                    .Where(r => r.Relation == TypingRelation.resist)
                    .Select(r => new TypingRelationlessDto { Id = r.RelatedTyping.Id, Name = r.RelatedTyping.Name })
                    .ToList(),
                Immunities = typing.Relationships
                    .Where(r => r.Relation == TypingRelation.immune)
                    .Select(r => new TypingRelationlessDto { Id = r.RelatedTyping.Id, Name = r.RelatedTyping.Name })
                    .ToList()
            };
        }
    }
}
