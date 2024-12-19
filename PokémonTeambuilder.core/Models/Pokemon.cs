namespace PokémonTeambuilder.core.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public Gender Gender { get; set; }
        public Item? Item { get; set; }
        public Nature Nature { get; set; }
        public int selectedAbilitySlot { get; set; }
        public ICollection<SelectedMove> SelectedMoves { get; set; }

        public int BasePokemonId { get; set; }
        public BasePokemon BasePokemon { get; set; }

        public int EVsId { get; set; }
        public Stats EVs { get; set; }

        public int IVsId { get; set; }
        public Stats IVs { get; set; }
    }
}
