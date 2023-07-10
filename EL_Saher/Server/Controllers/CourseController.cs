using EL_Saher.Server.Services;
using EL_Saher.Shared;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EL_Saher.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly IManager manager;
        public CourseController(IManager _manager)
        {
            manager = _manager;
        }

        [HttpPost("AddNewCourse")]
        public async Task<IActionResult> AddNewCourse([FromBody] CourseInfo newCourse)
        {
            await manager.AddNewCourse(newCourse);
            return Ok();
        }



        [HttpGet("GetAllCourses")]
        public async Task<List<Course>> GetAllCourses()
        {
            return await manager.GetAllCourses();
        }

        [HttpGet("GetStudentsByCourse/{_CourseId}")]
        public async Task<List<Student>> GetStudentsByCourse(int _CourseId)
        {
            return await manager.GetAllStudentsByCourseId(_CourseId);
        }
		[HttpPut("UpdateCourse/{courseId}")]
		public async Task<IActionResult> UpdateCourse([FromBody]CourseInfo course,[FromRoute] int courseId)
        {
            await manager.UpdateCourse(course, courseId);
            return Ok();
        }
        [HttpDelete("DeleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
			await manager.DeleteCourse(courseId);
			return Ok();
		}











	}
}
