namespace PokémonTeambuilder.core.Models
{
    public class Typing
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TypingRelationship> Relationships { get; set; }
    }
}