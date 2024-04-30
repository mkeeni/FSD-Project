namespace Mavericks_Bank.Exceptions
{
    public class NoAccountsFoundException : Exception
    {
        public NoAccountsFoundException()
        {
        }

        public NoAccountsFoundException(string? message) : base(message)
        {

        }
    }
}
