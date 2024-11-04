namespace PokémonTeambuilder.core.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Typing Typing { get; set; }
        public int? BasePower { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public ICollection<BasePokemon> BasePokemons { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; }
    }
}
