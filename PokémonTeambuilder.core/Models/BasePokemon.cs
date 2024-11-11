namespace PokémonTeambuilder.core.Models
{
    public class BasePokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Typing> Typings { get; set; }
        public ICollection<BasePokemonAbility> Abilities { get; set; }
        public Stats BaseStats { get; set; }
        public ICollection<Move> Moves { get; set; }
        public string Sprite { get; set; }
    }
}
