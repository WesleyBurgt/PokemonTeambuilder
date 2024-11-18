namespace PokémonTeambuilder.core.Exceptions
{
    public class ApiResponseException : Exception
    {
        public ApiResponseException(string message) : base(message) { }
        public ApiResponseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
