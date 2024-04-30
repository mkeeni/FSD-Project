namespace Mavericks_Bank.Exceptions
{
    public class NoLoansFoundException : Exception
    {
        public NoLoansFoundException(string? message) : base(message)
        {

        }
    }
}
