namespace PokémonTeambuilder.core.Classes
{
    public class Typing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TypingRelationless> Weaknesses { get; set; }
        public List<TypingRelationless> Resistances { get; set; }
        public List<TypingRelationless> Immunities { get; set; }
    }
}
