using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        [HttpPost("SetTeamName")]
        public async Task<IActionResult> SetTeamName([FromBody] SetTeamNameRequest request)
        {
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                await teamService.SetTeamNameAsync(request.TeamId, request.TeamName);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        public class SetTeamNameRequest
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
        }

        [HttpPost("AddPokemonToTeam")]
        public async Task<IActionResult> AddPokemonToTeam([FromBody] AddPokemonRequest request)
        {
            Pokemon pokemon;
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                pokemon = await pokemonService.CreatePokemonAsync(request.BasePokemonId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

            try
            {
                await teamService.AddPokemonToTeamAsync(request.TeamId, pokemon);

                PokemonDto response = MapPokemonToDto(pokemon);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await pokemonService.DeletePokemonAsync(pokemon.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        public class AddPokemonRequest
        {
            public int TeamId { get; set; }
            public int BasePokemonId { get; set; }
        }

        [HttpPost("DeletePokemonFromTeam")]
        public async Task<IActionResult> DeletePokemonFromTeam([FromBody] DeletePokemonRequest request)
        {
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                await teamService.RemovePokemonFromTeamAsync(request.TeamId, request.PokemonId);
                await pokemonService.DeletePokemonAsync(request.PokemonId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        public class DeletePokemonRequest
        {
            public int TeamId { get; set; }
            public int PokemonId { get; set; }
        }

        [HttpPost("UpdatePokemonFromTeam")]
        public async Task<IActionResult> UpdatePokemonFromTeam([FromBody] UpdatePokemonRequest request)
        {
            try
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(usernameClaim))
                {
                    return Unauthorized(new { message = "User identifier is missing from the token." });
                }

                Pokemon pokemon = MapDtoToPokemon(request.Pokemon);
                await pokemonService.UpdatePokemonAsync(pokemon);

                Pokemon newPokemon = await pokemonService.GetPokemonByIdAsync(pokemon.Id);
                PokemonDto response = MapPokemonToDto(newPokemon);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        public class UpdatePokemonRequest
        {
            public PokemonDto Pokemon { get; set; }
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
            List<SelectedMoveDto> selectedMoves = [];
            if(pokemon.SelectedMoves != null)
            {
                foreach(SelectedMove selectedMove in pokemon.SelectedMoves)
                {
                    SelectedMoveDto selectedMoveDto = MapSelectedMoveToDto(selectedMove);
                    selectedMoves.Add(selectedMoveDto);
                }
            }
            return new PokemonDto
            {
                PersonalId = pokemon.Id,
                Abilities = pokemon.BasePokemon.Abilities.Select(ability => MapBasePokemonAbilityToDto(ability)).ToList(),
                Ability = MapBasePokemonAbilityToDto(pokemon.BasePokemon.Abilities.FirstOrDefault(a => a.Slot == pokemon.selectedAbilitySlot)),
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

        private SelectedMoveDto MapSelectedMoveToDto(SelectedMove selectedMove)
        {
            return new SelectedMoveDto
            {
                Id = selectedMove.MoveId,
                Slot = selectedMove.Slot
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

        private Pokemon MapDtoToPokemon(PokemonDto dto)
        {
            List<SelectedMove> selectedMoves = [];
            if(dto.SelectedMoves != null)
            {
                foreach(SelectedMoveDto selectedMoveDto in dto.SelectedMoves)
                {
                    if (selectedMoveDto != null)
                    {
                        SelectedMove selectedMove = MapDtoToSelectedMove(selectedMoveDto, dto.Id);
                        selectedMoves.Add(selectedMove);
                    }
                }
            }
            Pokemon pokemon = new Pokemon
            {
                Id = dto.PersonalId,
                Nickname = dto.Nickname,
                Level = dto.Level,
                Gender = Enum.Parse<Gender>(dto.Gender),
                Item = MapDtoToItem(dto.Item),
                selectedAbilitySlot = dto.Ability.Slot,
                EVs = MapDtoToStats(dto.EVs),
                IVs = MapDtoToStats(dto.IVs),
                SelectedMoves = selectedMoves,
                BasePokemon = new BasePokemon
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Sprite = dto.Sprite,
                    Abilities = dto.Abilities?.Select(MapDtoToBasePokemonAbility).ToList(),
                    Typings = dto.Typings?.Select(MapDtoToBasePokemonTyping).ToList(),
                    Moves = dto.Moves?.Select(MapDtoToMove).ToList(),
                    BaseStats = MapDtoToStats(dto.BaseStats)
                },
                Nature = MapDtoToNature(dto.Nature)
            };
            foreach (BasePokemonAbility ability in pokemon.BasePokemon.Abilities)
            {
                ability.BasePokemon = pokemon.BasePokemon;
                ability.BasePokemonId = pokemon.BasePokemon.Id;
            }
            foreach (BasePokemonTyping typing in pokemon.BasePokemon.Typings)
            {
                typing.BasePokemon = pokemon.BasePokemon;
                typing.BasePokemonId = pokemon.BasePokemon.Id;
                foreach(TypingRelationship typingRelationship in typing.Typing.Relationships)
                {
                    typingRelationship.TypingId = typing.TypingId;
                }
            }
            return pokemon;
        }

        private SelectedMove MapDtoToSelectedMove(SelectedMoveDto selectedMoveDto, int pokemonId)
        {
            return new SelectedMove
            {
                MoveId = selectedMoveDto.Id,
                PokemonId = pokemonId,
                Slot = selectedMoveDto.Slot
            };
        }

        private Item MapDtoToItem(ItemDto dto)
        {
            if (dto == null) return null;

            return new Item
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image
            };
        }

        private Stats MapDtoToStats(StatsDto dto)
        {
            if (dto == null) return null;

            return new Stats
            {
                Hp = dto.Hp,
                Attack = dto.Attack,
                Defense = dto.Defense,
                SpecialAttack = dto.SpecialAttack,
                SpecialDefense = dto.SpecialDefense,
                Speed = dto.Speed
            };
        }

        private Move MapDtoToMove(MoveDto dto)
        {
            if (dto == null) return null;

            return new Move
            {
                Id = dto.Id,
                Name = dto.Name,
                Category = dto.Category,
                Accuracy = dto.Accuracy,
                BasePower = dto.BasePower,
                PP = dto.PP,
                Description = dto.Description,
                Typing = new Typing
                {
                    Id = dto.Typing.Id,
                    Name = dto.Typing.Name
                }
            };
        }

        private BasePokemonAbility MapDtoToBasePokemonAbility(AbilityDto dto)
        {
            return new BasePokemonAbility
            {
                Ability = new Ability
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description
                },
                AbilityId = dto.Id,
                IsHidden = dto.IsHidden,
                Slot = dto.Slot
            };
        }

        private Nature MapDtoToNature(NatureDto dto)
        {
            return new Nature
            {
                Id = dto.Id,
                Name = dto.Name,
                Up = string.IsNullOrEmpty(dto.Up) ? null : Enum.TryParse<StatsEnum>(dto.Up, true, out var upStat) ? upStat : throw new ArgumentException($"Invalid Up value: {dto.Up}"),
                Down = string.IsNullOrEmpty(dto.Down) ? null : Enum.TryParse<StatsEnum>(dto.Down, true, out var downStat) ? downStat : throw new ArgumentException($"Invalid Down value: {dto.Down}")
            };
        }

        private BasePokemonTyping MapDtoToBasePokemonTyping(BasePokemonTypingDto dto)
        {
            return new BasePokemonTyping
            {
                Slot = dto.Slot,
                TypingId = dto.Typing.Id,
                Typing = new Typing
                {
                    Id = dto.Typing.Id,
                    Name = dto.Typing.Name,
                    Relationships = dto.Typing.Weaknesses
                        .Select(w => new TypingRelationship { RelatedTypingId = w.Id, RelatedTyping = new Typing { Id = w.Id, Name = w.Name }, Relation = TypingRelation.weak })
                        .Concat(dto.Typing.Resistances
                            .Select(r => new TypingRelationship { RelatedTypingId = r.Id, RelatedTyping = new Typing { Id = r.Id, Name = r.Name }, Relation = TypingRelation.resist }))
                        .Concat(dto.Typing.Immunities
                            .Select(i => new TypingRelationship { RelatedTypingId = i.Id, RelatedTyping = new Typing { Id = i.Id, Name = i.Name }, Relation = TypingRelation.immune }))
                        .ToList()
                }
            };
        }
    }
}
