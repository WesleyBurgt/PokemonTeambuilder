namespace PokémonTeambuilder.core.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; }
    }
}
