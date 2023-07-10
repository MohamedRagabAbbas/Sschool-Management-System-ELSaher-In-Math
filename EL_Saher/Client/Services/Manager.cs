using EL_Saher.Client.Pages;
using EL_Saher.Shared;
using EL_Saher.Shared.Models.ServiceModels;
using System.Net.Http.Json;

namespace EL_Saher.Client.Services
{
	public class Manager: IManager
	{
        private readonly HttpClient httpClient;
        public Manager(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<Course>> GetCourses()
        {
            var courses =  await httpClient.GetFromJsonAsync<List<Course>>("api/Course/GetAllCourses");
            if (courses == null)
                return new List<Course>();
            else
                return courses;
        }
	
		public async Task<List<Student>> GetStudentsByCourse(int courseId)
        {
            var student = await httpClient.GetFromJsonAsync<List<Student>>($"api/Course/GetStudentsByCourse/{courseId}");
            if (student == null)
                return new List<Student>();
            else
                return student;
        }

        public async Task<HttpResponseMessage> AddCourseAsync(CourseInfo course)
        {
            return await httpClient.PostAsJsonAsync<CourseInfo>("api/Course/AddNewCourse", course);
        }

        public async Task<List<Student>> GetStudent()
        {
            var student = await httpClient.GetFromJsonAsync<List<Student>>("api/Student/GetAllStudents");
            if (student == null)
                return new List<Student>();
            else
                return student;
        }

        public async Task AddStudentAsync(StudentInfo student)
        {
            await httpClient.PostAsJsonAsync<StudentInfo>($"api/Student/AddNewStudent", student);

        }


        public async Task AddSession(AttendanceInfo attendance)
        {
            await httpClient.PostAsJsonAsync<AttendanceInfo>($"api/Attendance/AddSession", attendance);
        }

        public async Task UpdateAttendance(List<CourseAttendance> attendances)
        {
            await httpClient.PutAsJsonAsync<List<CourseAttendance>>($"api/Attendance/UpdateAttendances", attendances);
        }
        public async Task UpdateAtten(AttendanceInfo attendance, int id)
        {
            await httpClient.PutAsJsonAsync<AttendanceInfo>($"api/Attendance/UpdateAttendance/{id}", attendance);
        }

        public async Task AddAttendance(AttendanceInfo attendance)
        {
            await httpClient.PostAsJsonAsync<AttendanceInfo>($"api/Attendance/AddAttendance", attendance);
        }
        public async Task AddAttendances(List<AttendanceInfo> attendances)
        {
            await httpClient.PostAsJsonAsync<List<AttendanceInfo>>($"api/Attendance/AddAttendances", attendances);
        }

        public async Task DeleteSession(int AttendanceId)
        {
            await httpClient.DeleteAsync($"api/Attendance/DeleteSession/{AttendanceId}");
        }
        public async Task DeleteExam(int ExamId)
        {
            await httpClient.DeleteAsync($"api/Exam/DeleteExam/{ExamId}");
        }
        public async Task UpdateStudents(List<Student> students)
        {
            await httpClient.PutAsJsonAsync<List<Student>>($"api/Student/UpdateStudents", students);
        }

        public async Task UpdateStudent(StudentInfo student, int studentId)
        {
            await httpClient.PutAsJsonAsync<StudentInfo>($"api/Student/UpdateStudent/{studentId}", student);
        }
		public async Task UpdateCourse(CourseInfo course, int courseId)
		{
			await httpClient.PutAsJsonAsync<CourseInfo>($"api/Course/UpdateCourse/{courseId}", course);
		}

        public async Task DeleteCourseAsync(int courseId)
        {
            await httpClient.DeleteAsync($"api/Course/DeleteCourse/{courseId}");
        }
		public async Task AddExam(ExamInfo exam)
        {
            await httpClient.PostAsJsonAsync<ExamInfo>($"api/Exam/AddExam", exam);
        }

        public async Task UpdateExam(ExamInfo exam, int ExamId)
        {
            await httpClient.PutAsJsonAsync<ExamInfo>($"api/Exam/UpdateExam/{ExamId}", exam);

        }

        public async Task AddMonthFee(MonthFeeInfo monthFee)
        {
            await httpClient.PostAsJsonAsync<MonthFeeInfo>($"api/Fee/AddNewMonthFee", monthFee);
        }
        public async Task deleteStudent(int studentId)
        {
            await httpClient.DeleteAsync($"api/Student/DeleteStudent/{studentId}");
        }

        public async Task UpdateMonthFees(MonthFeeInfo monthFee, int Id)
        {
            await httpClient.PutAsJsonAsync<MonthFeeInfo>($"api/Fee/UpdateMonthFee/{Id}", monthFee);
        }
        public async Task DeleteMonthFee(int monthFeeId)
        {
            await httpClient.DeleteAsync($"api/Fee/DeleteMonthFee/{monthFeeId}");
        }










    }
}
