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
    public class BankEmployeesController : ControllerBase
    {
        private readonly IBankEmployeesAdminService _bankEmployeesService;
        private readonly ILogger<BankEmployeesController> _loggerBankEmployeesController;

        public BankEmployeesController(IBankEmployeesAdminService bankEmployeesService, ILogger<BankEmployeesController> loggerBankEmployeesController)
        {
            _bankEmployeesService = bankEmployeesService;
            _loggerBankEmployeesController = loggerBankEmployeesController;
        }

        [Authorize(Roles = "Admin")]
        [Route("GetAllBankEmployees")]
        [HttpGet]
        public async Task<ActionResult<List<BankEmployees>>> GetAllBankEmployees()
        {
            try
            {
                return await _bankEmployeesService.GetAllBankEmployees();
            }
            catch (NoBankEmployeesFoundException e)
            {
                _loggerBankEmployeesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetBankEmployee")]
        [HttpGet]
        public async Task<ActionResult<BankEmployees>> GetBankEmployee(int employeeID)
        {
            try
            {
                return await _bankEmployeesService.GetBankEmployee(employeeID);
            }
            catch (NoBankEmployeesFoundException e)
            {
                _loggerBankEmployeesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetEmployeeByEmail")]
        [HttpGet]
        public async Task<ActionResult<BankEmployees>> GetEmployeeByEmail(string email)
        {
            try
            {
                return await _bankEmployeesService.GetEmployeeByEmail(email);
            }
            catch (NoBankEmployeesFoundException e)
            {
                _loggerBankEmployeesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("UpdateBankEmployeeName")]
        [HttpPut]
        public async Task<ActionResult<BankEmployees>> UpdateBankEmployeeName(UpdateBankEmployeeNameDTO updateBankEmployeeNameDTO)
        {
            try
            {
                return await _bankEmployeesService.UpdateBankEmployeeName(updateBankEmployeeNameDTO);
            }
            catch (NoBankEmployeesFoundException e)
            {
                _loggerBankEmployeesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("DeleteBankEmployee")]
        [HttpDelete]
        public async Task<ActionResult<BankEmployees>> DeleteBankEmployee(int employeeID)
        {
            try
            {
                return await _bankEmployeesService.DeleteBankEmployee(employeeID);
            }
            catch (NoBankEmployeesFoundException e)
            {
                _loggerBankEmployeesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
