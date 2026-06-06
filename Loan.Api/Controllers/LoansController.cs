using Loan.Facade.Dtos;
using Loan.Facade.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Loan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ICreateLoanUseCase _create;

        public LoansController(ICreateLoanUseCase create) { _create = create; }

        [HttpPost]
        public async Task<IActionResult> CreateLoan(CreateBookLoanRequestDto request)
        {
            await _create.Execute(request);
            return Created();
        }
    }
}
