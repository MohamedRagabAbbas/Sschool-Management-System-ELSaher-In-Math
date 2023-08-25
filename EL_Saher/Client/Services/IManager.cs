using EL_Saher.Shared;
using EL_Saher.Shared.Models.ServiceModels;

namespace EL_Saher.Client.Services
{
	public interface IManager
	{
        public Task<List<Course>> GetCourses();
        public Task<List<Student>> GetStudent();
        public Task<List<Student>> GetStudentsByCourse(int courseId);

        public Task<HttpResponseMessage> AddCourseAsync(CourseInfo course);
        public Task UpdateCourse(CourseInfo course, int courseId);
        public Task DeleteCourseAsync(int courseId);
        public Task AddStudentAsync(StudentInfo student);
        public Task AddSession(AttendanceInfo attendance);
        public  Task UpdateStudentsGrade(string grade, int courseId);
		public Task UpdateAttendance(List<CourseAttendance> attendances);
        public Task UpdateAtten(AttendanceInfo attendances, int id);
        public Task AddAttendance(AttendanceInfo attendance);
        public Task AddAttendances(List<AttendanceInfo> attendances);
        public Task DeleteSession(int AttendanceId);
        public Task DeleteExam(int ExamId);

        public Task UpdateMonthFees(MonthFeeInfo monthFee, int Id);
        public Task DeleteMonthFee(int monthFeeId);



        public Task UpdateStudents(List<Student> students);

        public Task UpdateStudent(StudentInfo student, int studentId);

        public Task AddExam(ExamInfo exam);
        public Task UpdateExam(ExamInfo exam, int ExamId);

        public Task AddMonthFee(MonthFeeInfo monthFee);

        public Task deleteStudent(int studentId);

        //public async Task HandelMonthFees(string name, int fee, int studentId);






    }
}
