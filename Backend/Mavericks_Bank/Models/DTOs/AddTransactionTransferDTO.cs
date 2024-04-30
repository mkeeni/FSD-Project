namespace Mavericks_Bank.Models.DTOs
{
    public class AddTransactionTransferDTO
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int AccountID { get; set; }
        public int BeneficiaryID { get; set; }
    }
}
