namespace Mavericks_Bank.Exceptions
{
    public class NoBanksFoundException : Exception
    {
        public NoBanksFoundException(string? message) : base(message)
        {

        }
    }
}
