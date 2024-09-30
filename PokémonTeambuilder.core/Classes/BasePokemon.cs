namespace PokémonTeambuilder.core.Classes
{
    public class BasePokemon
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public List<Typing> Typings {  get; set; }
        public List<Ability> Abilities { get; set; }
        public Stats BaseStats { get; set; }
        public List<Move> Moves { get; set; }
        public string Sprite { get; set; }
    }
}
