namespace PokémonTeambuilder.core.Dto
{
    public class AbilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsHidden { get; set; }
        public int Slot { get; set; }
        public ICollection<BasePokemonDto> BasePokemons { get; set; }
    }
}
