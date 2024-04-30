namespace Mavericks_Bank.Exceptions
{
    public class TransactionAmountExceedsException : Exception
    {
        public TransactionAmountExceedsException(string? message) : base(message)
        {

        }
    }
}
