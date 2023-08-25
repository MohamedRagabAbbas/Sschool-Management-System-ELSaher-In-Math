using EL_Saher.Server.DataBase;
using EL_Saher.Shared;
using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;

namespace EL_Saher.Server.Services
{
    public interface IManager
    {
        public DbContextClass DbContext { get; set; }

        public Task AddNewCourse(CourseInfo newCourse);
        public Task AddNewStudent(StudentInfo newStudent);
        public Task AddNewExam(ExamInfo newExam);

        public Task<Student> GetStudent(int id);
        public Task<List<Student>> GetAllStudents();
        public Task<List<Course>> GetAllCourses();
        public Task<List<Student>> GetAllStudentsByCourseId(int CourseId);
        public Task<List<Student>> GetExcellentStudents(int CourseId);
        public Task UpdateCourse(CourseInfo course, int courseId);
        public Task DeleteStuent(int studentId);
        public Task DeleteCourse(int courseId);
        public Task AddSession(AttendanceInfo attendance);

        public Task<List<CourseAttendance>> GetAttendance(int studentId);
        public Task UpdateAttendance(List<CourseAttendance> attendances);
        public Task UpdateAttendance(AttendanceInfo attendance, int id);
        public Task AddAttendance(AttendanceInfo attendance);
        public Task AddAttendances(List<AttendanceInfo> attendances);
        public Task DeleteSession(int AttendanceId);
        public Task DeleteExam(int ExamId);

        public Task UpdateStudents(List<Student> students);
        public Task UpdateStudent(StudentInfo students, int studentId);
        public Task UpdateStudentsGrade(string grade, int courseId);


		public Task AddExam(ExamInfo exam);
        public Task UpdateExam(ExamInfo exam,int _ExamId);


        //public Task<List<MonthFee>> GetMonthFees(int studentId);
        //public Task UpdateMonthFees(List<MonthFee> monthFees);
        //public Task UpdateMonthFee(MonthFeeInfo monthFee, int id);

        public Task<List<MonthFee>> GetMonthFees(int studentId);
        public Task AddMonthFee(MonthFeeInfo monthFee);
        public  Task UpdateMonthFees(MonthFeeInfo monthFee, int Id);
        public Task DeleteMonthFee(int monthFeeId);


        public Task<ServiceResponse<Student>> LoginMobileApp(string UserName, string PhoneNumber);
        public Task<bool> ISValid(string UserName, string PhoneNumber);

        public Task HandelMonthFees(string name, int fee, int studentId);










    }
}
