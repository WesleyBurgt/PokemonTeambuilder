namespace PokémonTeambuilder.DTOs
{
    public class BasePokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BasePokemonTypingDto> Typings { get; set; }
        public List<AbilityDto> Abilities { get; set; }
        public StatsDto BaseStats { get; set; }
        public List<MoveDto> Moves { get; set; }
        public string Sprite { get; set; }
    }
}
