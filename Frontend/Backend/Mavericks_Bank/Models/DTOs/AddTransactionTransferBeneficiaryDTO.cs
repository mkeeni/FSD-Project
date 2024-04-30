namespace Mavericks_Bank.Models.DTOs
{
    public class AddTransactionTransferBeneficiaryDTO
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int AccountID { get; set; }
        public long BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public int BranchID { get; set; }
        public int CustomerID { get; set; }
    }
}
