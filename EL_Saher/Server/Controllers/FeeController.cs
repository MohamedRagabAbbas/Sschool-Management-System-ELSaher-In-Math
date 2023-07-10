using EL_Saher.Server.Services;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL_Saher.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        public readonly IManager manager;
        public FeeController(IManager _manager)
        {
            manager = _manager;
        }
        [HttpPost("AddNewMonthFee")]
        public async Task<IActionResult> AddNewMonthFee(MonthFeeInfo newMonthFee)
        {
            await manager.AddMonthFee(newMonthFee);
            return Ok();
        }
        [HttpPut("UpdateMonthFee/{Id}")]
        public async Task<IActionResult> UpdateMonthFee([FromBody] MonthFeeInfo newMonthFee, [FromRoute] int Id)
        {
            await manager.UpdateMonthFees(newMonthFee, Id);
            return Ok();
        }
        [HttpDelete("DeleteMonthFee/{Id}")]
        public async Task<IActionResult> DeleteMonthFee([FromRoute] int Id)
        {
            await manager.DeleteMonthFee(Id);
            return Ok();
        }

    }
}
