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
    public class LoansController : ControllerBase
    {
        private readonly ILoansAdminService _loanService;
        private readonly ILogger<LoansController> _loggerLoansController;

        public LoansController(ILoansAdminService loanService, ILogger<LoansController> loggerLoansController)
        {
            _loanService = loanService;
            _loggerLoansController = loggerLoansController;
        }

        [Authorize]
        [Route("GetAllLoans")]
        [HttpGet]
        public async Task<ActionResult<List<Loans>>> GetAllLoans()
        {
            try
            {
                return await _loanService.GetAllLoans();
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [Route("GetLoan")]
        [HttpGet]
        public async Task<ActionResult<Loans>> GetLoan(int loanID)
        {
            try
            {
                return await _loanService.GetLoan(loanID);
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddLoan")]
        [HttpPost]
        public async Task<Loans> AddLoan(Loans loan)
        {
            return await _loanService.AddLoan(loan);
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("UpdateLoanDetails")]
        [HttpPut]
        public async Task<ActionResult<Loans>> UpdateLoanDetails(Loans loan)
        {
            try
            {
                return await _loanService.UpdateLoanDetails(loan);
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteLoan")]
        [HttpDelete]
        public async Task<ActionResult<Loans>> DeleteLoan(int loanID)
        {
            try
            {
                return await _loanService.DeleteLoan(loanID);
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
