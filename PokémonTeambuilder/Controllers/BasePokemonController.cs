﻿using Microsoft.AspNetCore.Mvc;
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
    public class BasePokemonController : ControllerBase
    {
        private readonly BasePokemonService basePokemonService;

        public BasePokemonController(PokemonTeambuilderDbContext context)
        {
            basePokemonService = new BasePokemonService(new BasePokemonRepos(context));
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int offset, int limit)
        {
            try
            {
                List<BasePokemon> basePokemons = await basePokemonService.GetBasePokemonListAsync(offset, limit);
                int count = await basePokemonService.GetBasePokemonCountAsync();
                List<BasePokemonDto> basePokemonDtos = new List<BasePokemonDto>();

                foreach (BasePokemon basePokemon in basePokemons)
                {
                    basePokemonDtos.Add(MapBasePokemonToDto(basePokemon));
                }

                ApiListResponse response = new ApiListResponse
                {
                    Results = basePokemonDtos,
                    Count = count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private BasePokemonDto MapBasePokemonToDto(BasePokemon basePokemon)
        {
            return new BasePokemonDto
            {
                Id = basePokemon.Id,
                Name = basePokemon.Name,
                Abilities = basePokemon.Abilities.Select(ability => MapBasePokemonAbilityToDto(ability)).ToList(),
                BaseStats = MapStatsToDto(basePokemon.BaseStats),
                MoveIds = basePokemon.Moves.Select(move => move.Id).ToList(),
                Sprite = basePokemon.Sprite,
                Typings = basePokemon.Typings.Select(typing => MapBasePokemonTypingToDto(typing)).ToList()
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
