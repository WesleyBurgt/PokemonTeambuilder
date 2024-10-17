using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.apiwrapper;
using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.core.Dto;
using PokémonTeambuilder.core.Services;
using PokémonTeambuilder.DbContext;

namespace PokémonTeambuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : Controller
    {
        private readonly AppDbContext _context;

        public DatabaseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            BasePokemonService basePokemonService = new BasePokemonService(new BasePokemonWrapper());
            NatureService natureService = new NatureService(new NatureWrapper());
            TypingService typingService = new TypingService(new TypingWrapper());

            List<BasePokemon> basePokemonList = [];
            List<Typing> typingList = [];
            List<Nature> natureList = [];
            try
            {
                basePokemonList = await basePokemonService.GetPokemonListFromApi(0, 1);
                typingList = await typingService.GetAllTypingsFromApi();
                natureList = await natureService.GetAllNaturesFromApi();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            List<Ability> uniqueAbilities = GetUniqueAbilities(basePokemonList);
            List<Move> uniqureMoves = GetUniqueMoves(basePokemonList);


            List<BasePokemonDto> basePokemonDtos = [];
            List<TypingDto> typingDtos = [];
            List<NatureDto> natureDtos = [];
            List<AbilityDto> abilityDtos = [];
            List<MoveDto> moveDtos = [];

            Dictionary<int, TypingDto> mappedTypings = new Dictionary<int, TypingDto>();
            foreach (var typing in typingList)
            {
                TypingDto dto = MapTypingToDto(typing, mappedTypings);
                typingDtos.Add(dto);
            }
            foreach (Move move in uniqureMoves)
            {
                moveDtos.Add(MapMoveToDto(move, typingDtos));
            }
            foreach (Ability ability in uniqueAbilities)
            {
                abilityDtos.Add(MapAbilityToDto(ability));
            }
            foreach (BasePokemon basePokemon in basePokemonList)
            {
                List<TypingDto> filteredTypingList = typingDtos.FindAll(dto => basePokemon.Typings.Any(typing => dto.Id == typing.Id));
                List<AbilityDto> filteredAbilityList = abilityDtos.FindAll(dto => basePokemon.Abilities.Any(ability => dto.Id == ability.Id));
                List<MoveDto> filteredMoveList = moveDtos.FindAll(dto => basePokemon.Moves.Any(move => dto.Id == move.Id));

                BasePokemonDto basePokemonDto = MapBasePokemonToDto(basePokemon, filteredTypingList, filteredAbilityList, filteredMoveList);
                basePokemonDtos.Add(basePokemonDto);
                foreach (TypingDto typing in filteredTypingList)
                {
                    typing.BasePokemons.Add(basePokemonDto);
                }
            }
            foreach (Nature nature in natureList)
            {
                natureDtos.Add(MapNatureToDto(nature));
            }

            using (_context)
            {
                // Save Natures
                foreach (NatureDto natureDto in natureDtos)
                {
                    var existingNature = await _context.Natures
                        .AsNoTracking() // Prevent tracking to avoid conflicts
                        .FirstOrDefaultAsync(n => n.Id == natureDto.Id);

                    if (existingNature == null)
                    {
                        _context.Natures.Add(natureDto);
                    }
                    else
                    {
                        _context.Natures.Attach(natureDto); // Attach the existing entity and mark it for update
                        _context.Entry(natureDto).State = EntityState.Modified;
                    }
                }

                // Save Typings
                foreach (TypingDto typingDto in typingDtos)
                {
                    var existingTyping = await _context.Typings
                        .AsNoTracking()
                        .FirstOrDefaultAsync(t => t.Id == typingDto.Id);

                    if (existingTyping == null)
                    {
                        _context.Typings.Add(typingDto);
                    }
                    else
                    {
                        _context.Typings.Attach(typingDto);
                        _context.Entry(typingDto).State = EntityState.Modified;
                    }
                }

                // Save Moves
                foreach (MoveDto moveDto in moveDtos)
                {
                    var existingMove = await _context.Moves
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Id == moveDto.Id);

                    if (existingMove == null)
                    {
                        _context.Moves.Add(moveDto);
                    }
                    else
                    {
                        _context.Moves.Attach(moveDto);
                        _context.Entry(moveDto).State = EntityState.Modified;
                    }
                }

                // Save Abilities
                foreach (AbilityDto abilityDto in abilityDtos)
                {
                    var existingAbility = await _context.Abilities
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Id == abilityDto.Id);

                    if (existingAbility == null)
                    {
                        _context.Abilities.Add(abilityDto);
                    }
                    else
                    {
                        _context.Abilities.Attach(abilityDto);
                        _context.Entry(abilityDto).State = EntityState.Modified;
                    }
                }

                // Save BasePokemons
                foreach (BasePokemonDto basePokemonDto in basePokemonDtos)
                {
                    var existingBasePokemon = await _context.BasePokemons
                        .AsNoTracking()
                        .FirstOrDefaultAsync(bp => bp.Id == basePokemonDto.Id);

                    if (existingBasePokemon == null)
                    {
                        _context.BasePokemons.Add(basePokemonDto);
                    }
                    else
                    {
                        _context.BasePokemons.Attach(basePokemonDto);
                        _context.Entry(basePokemonDto).State = EntityState.Modified;
                    }
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message + "inner exception message: " + ex.InnerException.Message });
                }
            }


            return Ok();
        }

        private List<Ability> GetUniqueAbilities(List<BasePokemon> basePokemons)
        {
            List<Ability> uniqueAbilities = [];
            foreach (BasePokemon basePokemon in basePokemons)
            {
                foreach (Ability ability in basePokemon.Abilities)
                {
                    if (!uniqueAbilities.Any(a => a.Id == ability.Id))
                    {
                        uniqueAbilities.Add(ability);
                    }
                }
            }
            return uniqueAbilities;
        }

        private List<Move> GetUniqueMoves(List<BasePokemon> basePokemons)
        {
            List<Move> uniqueMoves = [];
            foreach (BasePokemon basePokemon in basePokemons)
            {
                foreach (Move move in basePokemon.Moves)
                {
                    if (!uniqueMoves.Any(a => a.Id == move.Id))
                    {
                        uniqueMoves.Add(move);
                    }
                }
            }
            return uniqueMoves;
        }

        private BasePokemonDto MapBasePokemonToDto(BasePokemon basePokemon, List<TypingDto> typingDtos, List<AbilityDto> abilityDtos, List<MoveDto> moveDtos)
        {
            List<TypingDto> typingList = typingDtos.FindAll(dto => basePokemon.Typings.Any(typing => dto.Id == typing.Id));
            List<AbilityDto> abilityList = abilityDtos.FindAll(dto => basePokemon.Abilities.Any(ability => dto.Id == ability.Id));
            List<MoveDto> moveList = moveDtos.FindAll(dto => basePokemon.Moves.Any(move => dto.Id == move.Id));

            return new BasePokemonDto
            {
                Id = basePokemon.Id,
                Name = basePokemon.Name,
                Typings = typingList,
                Abilities = abilityList,
                Moves = moveList,
                BaseStats = StatsToDto(basePokemon.BaseStats),
                Sprite = basePokemon.Sprite
            };
        }

        private NatureDto MapNatureToDto(Nature nature)
        {
            return new NatureDto
            {
                Id = nature.Id,
                Name = nature.Name,
                Up = nature.Up,
                Down = nature.Down
            };
        }

        private TypingDto MapTypingToDto(Typing typing, Dictionary<int, TypingDto> mappedTypings)
        {
            if (mappedTypings.ContainsKey(typing.Id))
            {
                return mappedTypings[typing.Id];
            }

            var typingDto = new TypingDto
            {
                Id = typing.Id,
                Name = typing.Name,
                Weaknesses = new List<TypingDto>(),
                Resistances = new List<TypingDto>(),
                Immunities = new List<TypingDto>(),
                BasePokemons = new List<BasePokemonDto>()
            };

            mappedTypings[typing.Id] = typingDto;

            foreach (var weakness in typing.Weaknesses)
            {
                var weaknessDto = MapRelationlessToDto(weakness, mappedTypings);
                typingDto.Weaknesses.Add(weaknessDto);
            }

            foreach (var resistance in typing.Resistances)
            {
                var resistanceDto = MapRelationlessToDto(resistance, mappedTypings);
                typingDto.Resistances.Add(resistanceDto);
            }

            foreach (var immunity in typing.Immunities)
            {
                var immunityDto = MapRelationlessToDto(immunity, mappedTypings);
                typingDto.Immunities.Add(immunityDto);
            }

            return typingDto;
        }

        private TypingDto MapRelationlessToDto(TypingRelationless relationless, Dictionary<int, TypingDto> mappedTypings)
        {
            if (mappedTypings.ContainsKey(relationless.Id))
            {
                return mappedTypings[relationless.Id];
            }

            var relationlessDto = new TypingDto
            {
                Id = relationless.Id,
                Name = relationless.Name,
                Weaknesses = new List<TypingDto>(),
                Resistances = new List<TypingDto>(),
                Immunities = new List<TypingDto>(),
                BasePokemons = new List<BasePokemonDto>()
            };

            mappedTypings[relationless.Id] = relationlessDto;

            return relationlessDto;
        }
        private AbilityDto MapAbilityToDto(Ability ability) 
        {
            return new AbilityDto
            {
                Id = ability.Id,
                Name = ability.Name,
                Description = ability.Description,
                IsHidden = false,
                Slot = 0,
                BasePokemons = []
            };
        }

        private MoveDto MapMoveToDto(Move move, List<TypingDto> typingDtos) 
        {
            TypingDto typing = typingDtos.Find(dto => move.Typing.Id == dto.Id);

            return new MoveDto
            {
                Id = move.Id,
                Name = move.Name,
                Description = move.Description,
                Accuracy = move.Accuracy,
                BasePower = move.BasePower,
                Typing = typing
            };
        }

        private StatsDto StatsToDto(Stats stats)
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
    }
}
