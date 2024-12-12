namespace PokémonTeambuilder.core.Models
{
    public class SelectedMove
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int MoveId { get; set; }
        public int Slot { get; set; }
    }
}
