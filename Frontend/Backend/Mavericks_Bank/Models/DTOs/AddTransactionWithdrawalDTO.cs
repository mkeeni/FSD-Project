namespace Mavericks_Bank.Models.DTOs
{
    public class AddTransactionWithdrawalDTO
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int AccountID { get; set; }
    }
}
