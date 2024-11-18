namespace PokémonTeambuilder.core.Exceptions
{
    public class InvalidVariableException : Exception
    {
        public object Variable { get; private set; }
        public Type? ParentType { get; private set; }

        public InvalidVariableException(string message, object variable) : base(message) 
        {
            Variable = variable;
            ParentType = null;
        }

        public InvalidVariableException(string message, object variable, Type parentType) : base(message)
        {
            Variable = variable;
            ParentType = parentType;
        }
    }
}
