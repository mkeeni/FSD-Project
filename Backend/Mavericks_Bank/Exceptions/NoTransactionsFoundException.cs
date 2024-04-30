namespace Mavericks_Bank.Exceptions
{
    public class NoTransactionsFoundException : Exception
    {
        public NoTransactionsFoundException(string? message) : base(message)
        {

        }
    }
}
