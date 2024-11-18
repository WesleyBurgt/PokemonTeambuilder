namespace PokémonTeambuilder.core.Exceptions
{
    public class InvalidIdException : Exception
    {
        public int Id { get; private set; }
        public Type? ParentType { get; private set; }

        public InvalidIdException(string message, int id) : base(message)
        {
            Id = id;
            ParentType = null;
        }

        public InvalidIdException(string message, int id, Type parentType) : base(message)
        {
            Id = id;
            ParentType = parentType;
        }
    }
}
