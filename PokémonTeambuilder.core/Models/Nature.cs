using PokémonTeambuilder.core.Enums;

namespace PokémonTeambuilder.core.Models
{
    public class Nature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatsEnum? Up { get; set; }
        public StatsEnum? Down { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; }
    }
}
