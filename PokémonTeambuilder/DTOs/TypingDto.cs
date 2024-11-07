namespace PokémonTeambuilder.DTOs
{
    public class TypingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TypingRelationlessDto> Weaknesses { get; set; }
        public List<TypingRelationlessDto> Resistances { get; set; }
        public List<TypingRelationlessDto> Immunities { get; set; }
    }
}
