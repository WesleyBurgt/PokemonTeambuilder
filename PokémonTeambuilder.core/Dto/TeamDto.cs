namespace PokémonTeambuilder.core.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonDto> Pokemons { get; set; }
    }
}
