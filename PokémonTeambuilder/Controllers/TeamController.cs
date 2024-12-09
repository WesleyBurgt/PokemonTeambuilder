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
        private readonly PokemonService pokemonService;

        public TeamController(PokemonTeambuilderDbContext context)
        {
            teamService = new TeamService(new TeamRepos(context));
            pokemonService = new PokemonService(new PokemonRepos(context), new BasePokemonRepos(context), new NatureRepos(context));
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

        [HttpPost("CreateTeam")]
        public async Task<IActionResult> CreateTeam()
        {
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                Team team = await teamService.CreateTeamAsync(usernameClaim);
                TeamDto response = MapTeamToDto(team);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("AddPokemonToTeam")]
        public async Task<IActionResult> AddPokemonToTeam([FromBody] AddPokemonRequest request)
        {
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                Pokemon pokemon = await pokemonService.CreatePokemonAsync(request.BasePokemonId);
                await teamService.AddPokemonToTeamAsync(request.TeamId, pokemon);

                PokemonDto response = MapPokemonToDto(pokemon);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        public class AddPokemonRequest
        {
            public int TeamId { get; set; }
            public int BasePokemonId { get; set; }
        }


        private TeamDto MapTeamToDto(Team team)
        {
            List<PokemonDto> pokemonDtos = [];
            if (team.Pokemons != null)
            {
                pokemonDtos = team.Pokemons.Select(pokemon => MapPokemonToDto(pokemon)).ToList();
            }
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Pokemons = pokemonDtos,
            };
        }

        private PokemonDto MapPokemonToDto(Pokemon pokemon)
        {
            ItemDto? item = MapItemToDto(pokemon.Item);
            List<MoveDto> selectedMoves = [];
            if(pokemon.SelectedMoves != null)
            {
                selectedMoves = pokemon.SelectedMoves.Select(move => MapMoveToDto(move)).ToList();
            }
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
                Item = item,
                Name = pokemon.BasePokemon.Name,
                Sprite = pokemon.BasePokemon.Sprite,
                Typings = pokemon.BasePokemon.Typings.Select(typing => MapBasePokemonTypingToDto(typing)).ToList(),
                Level = pokemon.Level,
                Moves = pokemon.BasePokemon.Moves.Select(move => MapMoveToDto(move)).ToList(),
                Nature = MapNatureToDto(pokemon.Nature),
                Nickname = pokemon.Nickname,
                SelectedMoves = selectedMoves
            };
        }

        private ItemDto? MapItemToDto(Item item)
        {
            if (item == null)
            {
                return null;
            }
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
