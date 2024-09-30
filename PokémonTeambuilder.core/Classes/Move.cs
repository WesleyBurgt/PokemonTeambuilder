namespace PokémonTeambuilder.core.Classes
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Typing Typing { get; set; }
        public int BasePower { get; set; }
        public int Accuracy { get; set; }
    }
}
