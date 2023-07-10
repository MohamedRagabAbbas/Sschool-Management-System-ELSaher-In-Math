using EL_Saher.Server.Services;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL_Saher.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IManager manager;
        public ExamController(IManager _manager)
        {
            manager = _manager;
        }

        [HttpPost("AddExam")]
        public async Task<IActionResult> AddExam([FromBody] ExamInfo newExam)
        {
            await manager.AddExam(newExam);
            return Ok();
        }
        [HttpPut("UpdateExam/{Id}")]
        public async Task<IActionResult> UpdateExam([FromBody] ExamInfo newExam,[FromRoute] int Id)
        {
            await manager.UpdateExam(newExam, Id);
            return Ok();
        }
        [HttpDelete("DeleteExam/{ExamId}")]
        public async Task<IActionResult> DeleteExam([FromRoute] int ExamId)
        {
            await manager.DeleteExam(ExamId);
            return Ok();
        }

    }
}
