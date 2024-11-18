namespace PokémonTeambuilder.core.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public int Amount { get; set; }
        public Range ExpectedRange { get; set; }

        public InvalidAmountException(string message, int amount, Range expectedRange) : base(message) 
        {
            Amount = amount;
            ExpectedRange = expectedRange;
        }
    }
}
