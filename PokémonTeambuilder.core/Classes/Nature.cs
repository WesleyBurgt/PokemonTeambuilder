namespace PokémonTeambuilder.core.Classes
{
    public class Nature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatsEnum? Up { get; set; }
        public StatsEnum? Down { get; set; }
    }
}
