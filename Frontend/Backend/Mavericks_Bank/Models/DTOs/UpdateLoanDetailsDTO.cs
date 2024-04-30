namespace Mavericks_Bank.Models.DTOs
{
    public class UpdateLoanDetailsDTO
    {
        public int LoanID { get; set; }
        public double LoanAmount { get; set; }
        public string LoanType { get; set; }
        public double Interest { get; set; }
        public int Tenure { get; set; }
    }
}
