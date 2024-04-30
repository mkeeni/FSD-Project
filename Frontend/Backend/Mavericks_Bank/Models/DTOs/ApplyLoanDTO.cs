namespace Mavericks_Bank.Models.DTOs
{
    public class ApplyLoanDTO
    {
        public double Amount { get; set; }
        public string Purpose { get; set; }
        public int LoanID { get; set; }
        public int CustomerID { get; set; }
    }
}
