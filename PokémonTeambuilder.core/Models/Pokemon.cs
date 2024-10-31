namespace PokémonTeambuilder.core.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public BasePokemon BasePokemon { get; set; }
        public Item? Item { get; set; }
        public Nature Nature { get; set; }
        public BasePokemonAbility Ability { get; set; }
        public ICollection<Move> SelectedMoves { get; set; }

        public int EVsId { get; set; }
        public Stats EVs { get; set; }

        public int IVsId { get; set; }
        public Stats IVs { get; set; }
    }
}
