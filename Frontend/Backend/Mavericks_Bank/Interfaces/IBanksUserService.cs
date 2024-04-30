using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IBanksUserService
    {
        public Task<List<Banks>> GetAllBanks();
        public Task<Banks> GetBank(int bankID);
    }
}
