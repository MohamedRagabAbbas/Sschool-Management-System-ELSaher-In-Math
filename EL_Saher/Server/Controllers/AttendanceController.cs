using EL_Saher.Server.Services;
using EL_Saher.Shared;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL_Saher.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public readonly IManager manager;
        public AttendanceController(IManager _manager)
        {
            manager = _manager;
        }

        [HttpGet("GetAttendance/studentId")]
        public async Task<List<CourseAttendance>> GetAttendance(int studentId)
        {
            return await manager.GetAttendance(studentId);
        }

        [HttpPost("AddSession")]
        public async Task<IActionResult> AddSession([FromBody] AttendanceInfo attendance)
        {
            await manager.AddSession(attendance);
            return Ok();
        }
        [HttpPost("AddAttendance")]
        public async Task<IActionResult> AddAttendance([FromBody] AttendanceInfo attendance)
        {
            await manager.AddAttendance(attendance);
            return Ok();
        }

        [HttpPost("AddAttendances")]
        public async Task<IActionResult> AddAttendances([FromBody] List<AttendanceInfo> attendances)
        {
            await manager.AddAttendances(attendances);
            return Ok();
        }

        [HttpPut("UpdateAttendances")]
        public async Task<IActionResult> UpdateAttendance([FromBody] List<CourseAttendance> attendances)
        {
            await manager.UpdateAttendance(attendances);
            return Ok();
        }

        [HttpPut("UpdateAttendance/{id}")]
        public async Task<IActionResult> UpdateAttendance( [FromRoute] int id ,[FromBody] AttendanceInfo attendances)
        {
            await manager.UpdateAttendance(attendances, id);
            return Ok();
        }

        [HttpDelete("DeleteSession/{AttendanceId}")]
        public async Task<IActionResult> DeleteSession(int AttendanceId)
        {
            await manager.DeleteSession(AttendanceId);
            return Ok();
        }

    }
}
