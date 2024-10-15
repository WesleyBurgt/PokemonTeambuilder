namespace PokémonTeambuilder.core.Dto
{
    public class BasePokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TypingDto> Typings { get; set; } // Many-to-many
        public ICollection<AbilityDto> Abilities { get; set; } // Many-to-many
        public ICollection<MoveDto> Moves { get; set; } // Many-to-many
        public StatsDto BaseStats { get; set; } // One-to-one
        public string Sprite { get; set; }
    }

}
