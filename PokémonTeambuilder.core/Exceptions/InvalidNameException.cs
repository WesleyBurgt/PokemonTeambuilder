namespace PokémonTeambuilder.core.Exceptions
{
    public class InvalidNameException : Exception
    {
        public string Name { get; private set; }
        public Type? Type { get; private set; }

        public InvalidNameException(string message, string name) : base(message)
        {
            Name = name;
            Type = null;
        }

        public InvalidNameException(string message, string name, Type type) : base(message)
        {
            Name = name;
            Type = type;
        }
    }
}
