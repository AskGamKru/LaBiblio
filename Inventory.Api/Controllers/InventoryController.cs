using Inventory.Facade.Dtos;
using Inventory.Facade.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class InventoryController : ControllerBase
    {
        private readonly IReserveBookUseCase _reserve;
        private readonly IReleaseBookUseCase _release;

        public InventoryController(IReserveBookUseCase reserve, IReleaseBookUseCase release)
        {
            _reserve = reserve;
            _release = release;
        }
        
        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve(ReserveBookRequestDto request)
        {
            var result = await _reserve.Execute(request);
            if (result.IsFailure)
            {
                return Ok(false);
            }
            return Ok(true);
        }
        
        [HttpPost("release")]
        public async Task<IActionResult> Release(ReleaseBookRequestDto request)
        {
            await _release.Execute(request);
            return Ok();
        }
    }
}
