namespace Mavericks_Bank.Models.DTOs
{
    public class UpdateCustomerDTO
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public long AadharNumber { get; set; }
        public string PANNumber { get; set; }
        public string Gender { get; set; }
    }
}
