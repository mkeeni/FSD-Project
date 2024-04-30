using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class AppliedLoansController : ControllerBase
    {
        private readonly IAppliedLoansAdminService _appliedLoansService;
        private readonly ILogger<AppliedLoansController> _loggerAppliedLoansController;

        public AppliedLoansController(IAppliedLoansAdminService appliedLoansService, ILogger<AppliedLoansController> loggerAppliedLoansController)
        {
            _appliedLoansService = appliedLoansService;
            _loggerAppliedLoansController = loggerAppliedLoansController;
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetAllAppliedLoans")]
        [HttpGet]
        public async Task<ActionResult<List<AppliedLoans>>> GetAllAppliedLoans()
        {
            try
            {
                return await _appliedLoansService.GetAllAppliedLoans();
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetAllAppliedLoansStatus")]
        [HttpGet]
        public async Task<ActionResult<List<AppliedLoans>>> GetAllAppliedLoansStatus(string status)
        {
            try
            {
                return await _appliedLoansService.GetAllAppliedLoansStatus(status);
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetAllCustomerAppliedLoans")]
        [HttpGet]
        public async Task<ActionResult<List<AppliedLoans>>> GetAllCustomerAppliedLoans(int customerID)
        {
            try
            {
                return await _appliedLoansService.GetAllCustomerAppliedLoans(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("GetAllCustomerAvailedLoans")]
        [HttpGet]
        public async Task<ActionResult<List<AppliedLoans>>> GetAllCustomerAvailedLoans(int customerID)
        {
            try
            {
                return await _appliedLoansService.GetAllCustomerAvailedLoans(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("GetAppliedLoan")]
        [HttpGet]
        public async Task<ActionResult<AppliedLoans>> GetAppliedLoan(int loanApplicationID)
        {
            try
            {
                return await _appliedLoansService.GetAppliedLoan(loanApplicationID);
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("AddAppliedLoan")]
        [HttpPost]
        public async Task<ActionResult<AppliedLoans>> AddAppliedLoan(ApplyLoanDTO applyLoanDTO)
        {
            try
            {
                return await _appliedLoansService.AddAppliedLoan(applyLoanDTO);
            }
            catch (NoLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (LoanAmountExceedsException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (AppliedLoanAlreadyExistsException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("UpdateAppliedLoanStatus")]
        [HttpPut]
        public async Task<ActionResult<AppliedLoans>> UpdateAppliedLoanStatus(int loanApplicationID, string status)
        {
            try
            {
                return await _appliedLoansService.UpdateAppliedLoanStatus(loanApplicationID, status);
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("DeleteAppliedLoan")]
        [HttpDelete]
        public async Task<ActionResult<AppliedLoans>> DeleteAppliedLoan(int loanApplicationID)
        {
            try
            {
                return await _appliedLoansService.DeleteAppliedLoan(loanApplicationID);
            }
            catch (NoAppliedLoansFoundException e)
            {
                _loggerAppliedLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
