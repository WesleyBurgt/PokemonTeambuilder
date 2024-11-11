using PokémonTeambuilder.core.Enums;

namespace PokémonTeambuilder.core.Models
{
    public class TypingRelationship
    {
        public int TypingId { get; set; }
        public Typing Typing { get; set; }

        public int RelatedTypingId { get; set; }
        public Typing RelatedTyping { get; set; }

        public TypingRelation Relation { get; set; }
    }
}
