using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface ICustomersAdminService:ICustomersUserService
    {
        public Task<List<Customers>> GetAllCustomers();
        public Task<Customers> GetCustomer(int customerID);
    }
}
