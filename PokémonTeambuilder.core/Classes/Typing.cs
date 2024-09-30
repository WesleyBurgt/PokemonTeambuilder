namespace PokémonTeambuilder.core.Classes
{
    public class Typing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Typing> Weaknesses { get; set; }
        public List<Typing> Resistances { get; set; }
        public List<Typing> Immunities { get; set; }
    }
}
