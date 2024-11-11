namespace PokémonTeambuilder.DTOs
{
    public class MoveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypingRelationlessDto Typing { get; set; }
        public int? BasePower { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
    }
}
