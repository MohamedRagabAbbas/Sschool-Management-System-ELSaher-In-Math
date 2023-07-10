using EL_Saher.Server.Services;
using EL_Saher.Shared;
using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL_Saher.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IManager manager;
        public StudentController(IManager _manager)
        {
            manager = _manager;
        }

        [HttpPost("AddNewStudent")]
        public async Task<IActionResult> AddNewStudent([FromBody] StudentInfo newStudent)
        {
            await manager.AddNewStudent(newStudent);
            return Ok();
        }


        [HttpGet("GetAllStudents")]
        public async Task<List<Student>> GetAllStudent()
        {
            return await manager.GetAllStudents();
        }
        [HttpGet("GetStudent/{studentId}")]
        public async Task<Student> GetStudent(int studentId)
        {
            return await manager.GetStudent(studentId);
        }

        [HttpPut("UpdateStudents")]
        public async Task<IActionResult> UpdateStudents([FromBody] List<Student> students)
        {
            await manager.UpdateStudents(students);
            return Ok();
        }

        [HttpPut("UpdateStudent/{studentId}")]
        public async Task<IActionResult> UpdateStudent([FromRoute]int studentId, [FromBody] StudentInfo student)
        {
            await manager.UpdateStudent(student, studentId);
            return Ok();
        }

        [HttpDelete("DeleteStudent/{Id}")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            await manager.DeleteStuent(Id);
            return Ok();
        }

        [HttpGet("GetExcellentStudents/{Id}")]
        public async Task<List<Student>> GetExcellentStudents(int Id)
        {
            return await manager.GetExcellentStudents(Id);
        }
        [HttpGet("LoginMobileApp/{Name}/{PhoneNumber}")]
        public async Task<ServiceResponse<Student>> LoginMobileApp(string Name, string PhoneNumber)
        {
            return await manager.LoginMobileApp(Name, PhoneNumber);
        }
        [HttpGet("ISValid/{Name}/{PhoneNumber}")]
        public async Task<bool> ISValid(string Name, string PhoneNumber)
        {
            return await manager.ISValid(Name, PhoneNumber);
        }

        

    }
}
