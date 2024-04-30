namespace Mavericks_Bank.Exceptions
{
    public class BankNameAlreadyExistsException : Exception
    {
        public BankNameAlreadyExistsException(string? message) : base(message)
        {

        }
    }
}
