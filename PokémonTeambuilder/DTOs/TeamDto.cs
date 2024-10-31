namespace PokémonTeambuilder.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PokemonDto> Pokemons { get; set; }
    }
}
