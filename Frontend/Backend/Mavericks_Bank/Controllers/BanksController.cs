using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBanksAdminService _banksService;
        private readonly ILogger<BanksController> _loggerBanksController;

        public BanksController(IBanksAdminService banksService, ILogger<BanksController> loggerBanksController)
        {
            _banksService = banksService;
            _loggerBanksController = loggerBanksController;
        }

        [Authorize]
        [Route("GetAllBanks")]
        [HttpGet]
        public async Task<ActionResult<List<Banks>>> GetAllBanks()
        {
            try
            {
                return await _banksService.GetAllBanks();
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("GetBank")]
        [HttpGet]
        public async Task<ActionResult<Banks>> GetBank(int bankID)
        {
            try
            {
                return await _banksService.GetBank(bankID);
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddBank")]
        [HttpPost]
        public async Task<ActionResult<Banks>> AddBank(Banks bank)
        {
            try
            {
                return await _banksService.AddBank(bank);
            }
            catch (BankNameAlreadyExistsException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateBankName")]
        [HttpPut]
        public async Task<ActionResult<Banks>> UpdateBankName(UpdateBankNameDTO updateBankNameDTO)
        {
            try
            {
                return await _banksService.UpdateBankName(updateBankNameDTO);
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (BankNameAlreadyExistsException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteBank")]
        [HttpDelete]
        public async Task<ActionResult<Banks>> DeleteBank(int bankID)
        {
            try
            {
                return await _banksService.DeleteBank(bankID);
            }
            catch (NoBanksFoundException e)
            {
                _loggerBanksController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
