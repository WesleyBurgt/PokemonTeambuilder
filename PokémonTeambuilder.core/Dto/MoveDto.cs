namespace PokémonTeambuilder.core.Dto
{
    public class MoveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BasePower { get; set; }
        public int Accuracy { get; set; }
        public TypingDto Typing { get; set; }
        public ICollection<BasePokemonDto> BasePokemons { get; set; } // Many-to-many relationship
        public ICollection<PokemonDto> Pokemons { get; set; } // Many-to-many relationship
    }
}
