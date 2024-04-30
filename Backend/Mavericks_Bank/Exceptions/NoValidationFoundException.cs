namespace Mavericks_Bank.Exceptions
{
    public class NoValidationFoundException : Exception
    {
        public NoValidationFoundException(string? message) : base(message)
        {
        }
    }
}
