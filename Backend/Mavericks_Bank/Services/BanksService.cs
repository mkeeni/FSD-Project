using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mavericks_Bank.Services
{
    public class BanksService : IBanksAdminService
    {
        private readonly IRepository<int,Banks> _banksRepository;
        private readonly ILogger<BanksService> _loggerBanksService;

        public BanksService(IRepository<int, Banks> banksRepository, ILogger<BanksService> loggerBanksService)
        {
            _banksRepository = banksRepository;
            _loggerBanksService = loggerBanksService;
        }

        public async Task<Banks> AddBank(Banks bank)
        {
            var allBanks = await _banksRepository.GetAll();
            if(allBanks != null)
            {
                if(allBanks.Contains(bank))
                {
                    throw new BankNameAlreadyExistsException($"Bank Name {bank.BankName} already exists");
                }
            }
            return await _banksRepository.Add(bank);
        }

        public async Task<Banks> DeleteBank(int bankID)
        {
            var deletedBank = await _banksRepository.Delete(bankID);
            if(deletedBank == null)
            {
                throw new NoBanksFoundException($"Bank ID {bankID} not found");
            }
            return deletedBank;
        }

        public async Task<List<Banks>> GetAllBanks()
        {
            var allBanks = await _banksRepository.GetAll();
            if(allBanks == null)
            {
                throw new NoBanksFoundException($"No Banks Data Found");
            }
            return allBanks;
        }

        public async Task<Banks> GetBank(int bankID)
        {
            var foundBank = await _banksRepository.Get(bankID);
            if(foundBank == null)
            {
                throw new NoBanksFoundException($"Bank ID {bankID} not found");
            }
            return foundBank;
        }

        public async Task<Banks> UpdateBankName(UpdateBankNameDTO updateBankNameDTO)
        {
            var foundBank = await GetBank(updateBankNameDTO.BankID);
            var allBanks = await _banksRepository.GetAll();
            if (allBanks != null)
            {
                var foundBankName = allBanks.FirstOrDefault(banks => banks.BankName == updateBankNameDTO.BankName);
                if (foundBankName != null)
                {
                    throw new BankNameAlreadyExistsException($"Bank Name {updateBankNameDTO.BankName} already exists");
                }
            }
            foundBank.BankName = updateBankNameDTO.BankName;
            var updatedBank = await _banksRepository.Update(foundBank);
            return updatedBank;
        }
    }
}
