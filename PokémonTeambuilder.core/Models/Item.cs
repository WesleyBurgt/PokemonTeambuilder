namespace PokémonTeambuilder.core.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; }
    }
}
