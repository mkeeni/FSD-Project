namespace Mavericks_Bank.Models.DTOs
{
    public class InboundAndOutboundTransactions
    {
        public int TotalTransactions { get; set; }
        public double InboundTransactions { get; set; }
        public double OutboundTransactions { get; set; }
        public double Ratio { get; set; }
        public string CreditWorthiness { get; set; }
    }
}
