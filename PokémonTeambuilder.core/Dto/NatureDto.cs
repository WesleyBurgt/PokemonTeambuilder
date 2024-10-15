using PokémonTeambuilder.core.Classes;

namespace PokémonTeambuilder.core.Dto
{
    public class NatureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatsEnum? Up { get; set; }
        public StatsEnum? Down { get; set; }
    }
}
