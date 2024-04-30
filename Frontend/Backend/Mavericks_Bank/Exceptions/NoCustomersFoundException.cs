namespace Mavericks_Bank.Exceptions
{
    public class NoCustomersFoundException : Exception
    {
        public NoCustomersFoundException(string? message) : base(message)
        {

        }
    }
}
