namespace PokémonTeambuilder.core.Models
{
    public class BasePokemonTyping
    {
        public int BasePokemonId { get; set; }
        public BasePokemon BasePokemon { get; set; }

        public int TypingId { get; set; }
        public Typing Typing { get; set; }

        public int Slot { get; set; }
    }
}
