namespace Mavericks_Bank.Models.DTOs
{
    public class UpdatedValidationDTO
    {
        public string Email { get; set; }
        public string UserType { get; set; }
        public string? Status { get; set; } = null;
    }
}
