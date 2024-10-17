using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.Dto;

namespace PokémonTeambuilder.dal
{
    public interface IAppDbContext
    {
        DbSet<BasePokemonDto> BasePokemons { get; }
        DbSet<PokemonDto> Pokemons { get; }
        DbSet<TeamDto> Teams { get; }
        DbSet<AbilityDto> Abilities { get; }
        DbSet<MoveDto> Moves { get; }
        DbSet<StatsDto> Stats { get; }
        DbSet<TypingDto> Typings { get; }
        DbSet<ItemDto> Items { get; }
        DbSet<NatureDto> Natures { get; }
    }
}
