namespace PokémonTeambuilder.core.Classes
{
    public class Pokemon : BasePokemon
    {
        public int PersonalId { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public Item? Item { get; set; }
        public Nature Nature { get; set; }
        public Ability Ability { get; set; }
        public List<Move> SelectedMoves { get; set; }
        public Stats EVs { get; set; }
        public Stats IVs { get; set; }
    }
}
