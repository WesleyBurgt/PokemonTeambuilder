namespace PokémonTeambuilder.core.Exceptions
{
    public class ReposResponseException : Exception
    {
        public ReposResponseException(string message) : base(message) { }
        public ReposResponseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
