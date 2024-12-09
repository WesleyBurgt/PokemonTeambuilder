using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokémonTeambuilder.core.Enums;
using PokémonTeambuilder.core.Models;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.dal.DbContext;
using PokémonTeambuilder.dal.Repos;
using PokémonTeambuilder.DTOs;
using System.Security.Claims;

namespace PokémonTeambuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService teamService;

        public TeamController(PokemonTeambuilderDbContext context)
        {
            teamService = new TeamService(new TeamRepos(context));
        }

        [HttpGet("GetTeams")]
        public async Task<IActionResult> GetTeams()
        {
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                // Call the service to get teams by user
                List<Team> teams = await teamService.GetTeamsByUsernameAsync(usernameClaim);
                int count = await teamService.GetTeamCountByUsernameAsync(usernameClaim);

                List<TeamDto> teamDtos = teams.Select(MapTeamToDto).ToList();

                var response = new ApiListResponse
                {
                    Results = teamDtos,
                    Count = count
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private TeamDto MapTeamToDto(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Pokemons = team.Pokemons.Select(pokemon => MapPokemonToDto(pokemon)).ToList(),
            };
        }

        private PokemonDto MapPokemonToDto(Pokemon pokemon)
        {
            return new PokemonDto
            {
                PersonalId = pokemon.Id,
                Abilities = pokemon.BasePokemon.Abilities.Select(ability => MapBasePokemonAbilityToDto(ability)).ToList(),
                Ability = MapBasePokemonAbilityToDto(pokemon.Ability),
                BaseStats = MapStatsToDto(pokemon.BasePokemon.BaseStats),
                EVs = MapStatsToDto(pokemon.EVs),
                IVs = MapStatsToDto(pokemon.IVs),
                Gender = pokemon.Gender.ToString(),
                Id = pokemon.BasePokemon.Id,
                Item = MapItemToDto(pokemon.Item),
                Level = pokemon.Level,
                Moves = pokemon.SelectedMoves.Select(move => MapMoveToDto(move)).ToList(),
            };
        }

        private BasePokemonDto MapBasePokemonToDto(BasePokemon basePokemon)
        {
            return new BasePokemonDto
            {
                Id = basePokemon.Id,
                Name = basePokemon.Name,
                Abilities = basePokemon.Abilities.Select(ability => MapBasePokemonAbilityToDto(ability)).ToList(),
                BaseStats = MapStatsToDto(basePokemon.BaseStats),
                Moves = basePokemon.Moves.Select(move => MapMoveToDto(move)).ToList(),
                Sprite = basePokemon.Sprite,
                Typings = basePokemon.Typings.Select(typing => MapBasePokemonTypingToDto(typing)).ToList()
            };
        }

        private ItemDto MapItemToDto(Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Description = item.Description,
                Image = item.Image,
                Name = item.Name
            };
        }

        private AbilityDto MapBasePokemonAbilityToDto(BasePokemonAbility basePokemonAbility)
        {
            return new AbilityDto
            {
                Id = basePokemonAbility.Ability.Id,
                Name = basePokemonAbility.Ability.Name,
                Description = basePokemonAbility.Ability.Description,
                IsHidden = basePokemonAbility.IsHidden,
                Slot = basePokemonAbility.Slot
            };
        }

        private StatsDto MapStatsToDto(Stats stats)
        {
            return new StatsDto
            {
                Hp = stats.Hp,
                Attack = stats.Attack,
                Defense = stats.Defense,
                SpecialAttack = stats.SpecialAttack,
                SpecialDefense = stats.SpecialDefense,
                Speed = stats.Speed
            };
        }

        private MoveDto MapMoveToDto(Move move)
        {
            return new MoveDto
            {
                Id = move.Id,
                Name = move.Name,
                Category = move.Category,
                Accuracy = move.Accuracy,
                BasePower = move.BasePower,
                PP = move.PP,
                Description = move.Description,
                Typing = new TypingRelationlessDto { Id = move.Typing.Id, Name = move.Typing.Name }
            };
        }

        private BasePokemonTypingDto MapBasePokemonTypingToDto(BasePokemonTyping basePokemonTyping)
        {
            return new BasePokemonTypingDto
            {
                Slot = basePokemonTyping.Slot,
                Typing = new TypingDto
                {
                    Id = basePokemonTyping.Typing.Id,
                    Name = basePokemonTyping.Typing.Name,

                    Weaknesses = basePokemonTyping.Typing.Relationships
                    .Where(r => r.Relation == TypingRelation.weak)
                    .Select(r => new TypingRelationlessDto { Id = r.RelatedTyping.Id, Name = r.RelatedTyping.Name })
                    .ToList(),
                    Resistances = basePokemonTyping.Typing.Relationships
                    .Where(r => r.Relation == TypingRelation.resist)
                    .Select(r => new TypingRelationlessDto { Id = r.RelatedTyping.Id, Name = r.RelatedTyping.Name })
                    .ToList(),
                    Immunities = basePokemonTyping.Typing.Relationships
                    .Where(r => r.Relation == TypingRelation.immune)
                    .Select(r => new TypingRelationlessDto { Id = r.RelatedTyping.Id, Name = r.RelatedTyping.Name })
                    .ToList()
                }
            };
        }
    }
}
