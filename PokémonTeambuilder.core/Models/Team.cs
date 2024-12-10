namespace PokémonTeambuilder.core.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int PokemonCount { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; }
    }
}
