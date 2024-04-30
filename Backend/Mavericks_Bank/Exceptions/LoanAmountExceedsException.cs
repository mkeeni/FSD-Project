namespace Mavericks_Bank.Exceptions
{
    public class LoanAmountExceedsException : Exception
    {
        public LoanAmountExceedsException(string? message) : base(message)
        {

        }
    }
}
