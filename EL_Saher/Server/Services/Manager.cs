using Azure.Core;
using EL_Saher.Client.Pages;
using EL_Saher.Server.DataBase;
using EL_Saher.Shared;
using EL_Saher.Shared.Models;
using EL_Saher.Shared.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using static System.Net.WebRequestMethods;
using Exam = EL_Saher.Shared.Exam;

namespace EL_Saher.Server.Services
{
    public class Manager:IManager
    {
        public DbContextClass DbContext { get; set; }
        public Manager(DbContextClass _DbContext)
        {
            DbContext = _DbContext;
        }

		// Adding Methods
		public async Task<List<Student>> GetExcellentStudents(int CourseId)
		{
			var Course = DbContext.Courses.Find(CourseId);

			if (Course != null)
			{
				List<Student> Students = await DbContext.Students.Where(x => x.CourseId == CourseId).Include(x => x.Exams).ToListAsync();
				List<KeyValuePair<Student, decimal>> scores = new List<KeyValuePair<Student, decimal>>();

				foreach (var student in Students)
				{
					decimal ExamsScore = 0;
					int counter = 0;
					if (student.Exams != null)
					{
						foreach (var Exam in student.Exams)
						{
							if (Exam != null)
							{
								if (Exam.OutOf == 0) Exam.OutOf = 1;
								ExamsScore += (Exam.Score) / (Exam.OutOf);
								counter += 1;
							}
						}
						if (counter != 0)
							ExamsScore /= counter;
						scores.Add(new KeyValuePair<Student, decimal>(student, ExamsScore));
					}
				}
				scores.Sort((x, y) => y.Value.CompareTo(x.Value));
				List<Student> ExcellentStudents = new List<Student>();
				for (int i = 0; i < scores.Count / 3 || i < Course.StudentsNumber; i++)
				{
					ExcellentStudents.Add(scores[i].Key);
				}
				return ExcellentStudents;
			}
			else
			{
				return new List<Student>();
			}
		}
		public async Task AddNewCourse(CourseInfo newCourse)
        {
            var NewCourse = new Course() { Name = newCourse.Name, Cost = newCourse.Cost, Schedule = newCourse.Schedule, Grade = newCourse.Grade };
            await DbContext.Courses.AddAsync(NewCourse);
            await DbContext.SaveChangesAsync();
        }
        public async Task AddNewStudent(StudentInfo newStudent)
        {
            var course = await DbContext.Courses.FindAsync(newStudent.CourseID);
            //var student = await DbContext.Students.Include(x=>x.Attendances).Include(x => x.MonthFees).Include(x => x.Exams).FirstOrDefaultAsync(a => a.CourseId == newStudent.CourseID);
            if(course!=null)
            {
                var NewStudent = new Student()
                {
                    Name = newStudent.Name,
                    ContactNumber = newStudent.ContactNumber,
                    StudentGrade = newStudent.StudentGrade,
                    StudentRate = newStudent.StudentRate,
                    CourseId = newStudent.CourseID
                };

                await DbContext.Students.AddAsync(NewStudent);
                DbContext.SaveChanges();
            }
        }
        public async Task<Student> GetStudent(int id)
        {
            var student =  await DbContext.Students.Include(s=>s.Course).Include(s=>s.Attendances).Include(s=>s.Exams).Include(x=>x.MonthFees).Where(s=>s.StudentID==id).FirstAsync();
            if(student!=null)
            {
                return student;
            }
            else
            {
                return new Student();
            }
        }

        public async Task DeleteStuent(int studentId)
        {
            var student = DbContext.Students.Include(x=>x.Attendances).Include(x => x.Exams).Include(x => x.MonthFees).Where(s=>s.StudentID==studentId).First();
            if(student!=null)
            {
                if(student.Exams!=null)
                {
                    DbContext.Exams.RemoveRange(student.Exams);
                    await DbContext.SaveChangesAsync();
                }
                if(student.Attendances!=null)
                {
                    DbContext.CourseAttendances.RemoveRange(student.Attendances);
                    await DbContext.SaveChangesAsync();
                }
                if(student.MonthFees!=null)
                {
                    DbContext.MonthFees.RemoveRange(student.MonthFees);
                    await DbContext.SaveChangesAsync();
                }
                DbContext.Students.Remove(student);
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteCourse(int courseId)
        {
			var course = DbContext.Courses.Find(courseId);
			if(course!=null)
            {
				DbContext.Courses.Remove(course);
				await DbContext.SaveChangesAsync();
			}
		}

        public async Task AddNewExam(ExamInfo newExam)
        {
            var NewExam = new Exam() { Name = newExam.Name, Date = newExam.Date, OutOf = newExam.OutOf };
            await DbContext.Exams.AddAsync(NewExam);
            DbContext.SaveChanges();
        }
        public async Task DeleteExam(int ExamId)
        {
            var exam = DbContext.Exams.Find(ExamId);
            if (exam != null)
            {
                DbContext.Exams.Remove(exam);
                await DbContext.SaveChangesAsync();
            }
        }



        public async Task<List<Student>> GetAllStudents()
        {
            var Students = await DbContext.Students.Include(x=>x.Exams).ToListAsync();
            return Students;
        }
        public async Task<List<Course>> GetAllCourses()
        {
            var Courses = await DbContext.Courses.Include(c=>c.Students).ToListAsync();
            return Courses;
        }

        public async Task<List<Student>> GetAllStudentsByCourseId(int CourseId)
        {

            var Course = await DbContext.Courses.FindAsync(CourseId);
            if (Course != null)
            {
                var Students = await DbContext.Students.Include(x=>x.Attendances).Include(x=>x.Exams).Include(x=>x.MonthFees).Where(x => x.CourseId == CourseId).ToListAsync();
                if (Students != null)
                    return Students;
                else
                    return new List<Student>();
            }
            else
            {
                return new List<Student>();
            }

        }
        public async Task AddSession(AttendanceInfo attendance)
        {
            var _attendance = new CourseAttendance() {  StudentId = attendance.StudentId, SessionNumber = attendance.SessionNumber };
            await DbContext.CourseAttendances.AddAsync(_attendance);
            await DbContext.SaveChangesAsync();
        }
        public async Task<List<CourseAttendance>> GetAttendance(int studentId)
        {
            var student = DbContext.Students.Include(x=>x.Attendances).SingleOrDefault(x => x.StudentID == studentId);
            if (student != null)
            {
                return student.Attendances.ToList();
            }
            else
                return new List<CourseAttendance>();
        }

        public async Task UpdateAttendance(List<CourseAttendance> attendances)
        {
            DbContext.CourseAttendances.UpdateRange(attendances);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAttendance(AttendanceInfo attendance, int id)
        {
            var _attendance = await DbContext.CourseAttendances.FindAsync(id);
            _attendance.SessionNumber = attendance.SessionNumber;
            _attendance.StudentId = attendance.StudentId;
            _attendance.AttendanceStatus = attendance.AttendanceStatus;
            DbContext.CourseAttendances.Update(_attendance);
            await DbContext.SaveChangesAsync();
        }


        public async Task UpdateStudents(List<Student> students)
        {
            DbContext.Students.UpdateRange(students);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateStudent(StudentInfo student, int studentId)
        {
            var _student = new Student()
            {
                StudentID = studentId,
                Name = student.Name,
                ContactNumber = student.ContactNumber,
                StudentGrade = student.StudentGrade,
                StudentRate = student.StudentRate,
                CourseId = student.CourseID
            };
			DbContext.Students.Update(_student);
			DbContext.SaveChanges();
		}

		public async Task UpdateCourse(CourseInfo course, int courseId)
        {

            var _course = new Course()
            {
                CourseID = courseId,
                Name = course.Name,
                Cost = course.Cost,
                Schedule = course.Schedule,
                Grade = course.Grade, 
                StudentsNumber = course.number
			};
			DbContext.Courses.Update(_course);
			await DbContext.SaveChangesAsync();
        }

		public async Task UpdateStudentsGrade(string grade, int courseId)
        {
            var students = await GetAllStudentsByCourseId(courseId);
            if(students!=null)
            {
                foreach(var student in students)
                {
					student.StudentGrade = grade;
				}
                DbContext.UpdateRange(students);
                await DbContext.SaveChangesAsync();
            }
        }



		public async Task AddAttendance(AttendanceInfo attendance)
        {
            CourseAttendance courseAttendance = new CourseAttendance()
            {
                AttendanceDate = DateTime.Today,
                AttendanceStatus = attendance.AttendanceStatus,
                SessionNumber = attendance.SessionNumber,
                StudentId = attendance.StudentId,
            };
            await DbContext.CourseAttendances.AddAsync(courseAttendance);
            await DbContext.SaveChangesAsync();
        }
        public async Task DeleteSession(int AttendanceId)
        {
            var attendance = DbContext.CourseAttendances.Find(AttendanceId);
            if(attendance!=null)
            {
                DbContext.CourseAttendances.Remove(attendance);
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task AddAttendances(List<AttendanceInfo> attendances)
        {
            List<CourseAttendance> courseAttendances = new List<CourseAttendance>();
            foreach (var attendance in attendances)
            {
                CourseAttendance courseAttendance = new CourseAttendance()
                {
                    AttendanceDate = DateTime.Today,
                    AttendanceStatus = attendance.AttendanceStatus,
                    SessionNumber = attendance.SessionNumber,
                    StudentId = attendance.StudentId,
                };
                courseAttendances.Add(courseAttendance);

            }
            await DbContext.CourseAttendances.AddRangeAsync(courseAttendances);
            await DbContext.SaveChangesAsync();
        }


        public async Task AddExam(ExamInfo exam)
        {
            bool test = false;
            int studentId = exam.StudentId;
            var student = DbContext.Students.Include(x=>x.Exams).First(c=>c.StudentID == studentId);
            if(student!=null)
            {
                if(student.Exams!=null)
                {
                    foreach(var e in student.Exams)
                    {
                        if(e!=null)
                        {
                            if(e.Name == exam.Name && e.OutOf == exam.OutOf)
                            {
                                test = true; break;
                            }
                        }
                    }
                }
            }
            if(test==false)
            {
				Exam _exam = new Exam()
				{
					Date = exam.Date,
					Name = exam.Name,
					OutOf = exam.OutOf,
					StudentId = exam.StudentId,
					Score = exam.Score

				};
				await DbContext.Exams.AddAsync(_exam);
				await DbContext.SaveChangesAsync();
			}
        }
        public async Task UpdateExam(ExamInfo exam, int _ExamId)
        {
            Exam _exam = new Exam()
            {
                Date = exam.Date,
                Name = exam.Name,
                OutOf = exam.OutOf,
                StudentId = exam.StudentId,
                Score = exam.Score, 
                ExamID = _ExamId
            };
            DbContext.Exams.Update(_exam);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<MonthFee>> GetMonthFees(int studentId)
        {
            var student = DbContext.Students.Include(x => x.MonthFees).SingleOrDefault(x => x.StudentID == studentId);
            if (student != null)
            {
                return student.MonthFees.ToList();
            }
            else
                return new List<MonthFee>();
        }

        public async Task AddMonthFee(MonthFeeInfo monthFee)
        {
			bool test = false;
			int studentId = monthFee.StudentId;
			var student = DbContext.Students.Include(x => x.MonthFees).First(c => c.StudentID == studentId);
			if (student != null)
			{
				if (student.MonthFees != null)
				{
					foreach (var e in student.MonthFees)
					{
						if (e != null)
						{
							if (e.Name == monthFee.Name && e.Fees == monthFee.Fees)
							{
								test = true; break;
							}
						}
					}
				}
			}
			if (test == false)
			{
				MonthFee _monthFee = new MonthFee()
				{
					IsPaid = monthFee.IsPaid,
					Month = monthFee.Month,
					StudentId = monthFee.StudentId,
					Fees = monthFee.Fees,
					Name = monthFee.Name,
				};
				await DbContext.MonthFees.AddAsync(_monthFee);
				await DbContext.SaveChangesAsync();
			}
        }

        public async Task UpdateMonthFees(MonthFeeInfo monthFee, int Id)
        {
            MonthFee month = new MonthFee()
            {
                Fees = monthFee.Fees,
                IsPaid = monthFee.IsPaid,
                Month = monthFee.Month,
                Name = monthFee.Name,
                StudentId = monthFee.StudentId,
                MonthFeeId = Id

            };
            DbContext.MonthFees.Update(month);
            await DbContext.SaveChangesAsync();
        }
        public async Task DeleteMonthFee(int monthFeeId)
        {
            var monthFee = DbContext.MonthFees.Find(monthFeeId);
            if(monthFee != null)
            {
                DbContext.MonthFees.Remove(monthFee);
                await DbContext.SaveChangesAsync();
            }
        }
        public async Task<ServiceResponse<Student>> LoginMobileApp(string UserName, string PhoneNumber)
        {
            var student = await DbContext.Students.Include(s=>s.Course).Include(s => s.Attendances).Include(s => s.Exams).Include(s => s.MonthFees).SingleOrDefaultAsync(x => x.Name == UserName && x.ContactNumber == PhoneNumber);
            ServiceResponse<Student> serviceResponse = new ServiceResponse<Student>();
            if (student!=null)
            {
                serviceResponse.Data = student;
                serviceResponse.Message = "تم التسجيل بنجاح";
                serviceResponse.IsSuccess = true;
                return serviceResponse;
            }
            else
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "الاسم او رقم الهاتف غير صحيح";
                serviceResponse.IsSuccess = false;
                return serviceResponse;
            }
        }
        public async Task<bool> ISValid(string UserName, string PhoneNumber)
        {
            var result = await DbContext.Students.AnyAsync(x => x.Name == UserName && x.ContactNumber == PhoneNumber);
            return result;
        }

        public async Task HandelMonthFees(string name, int fee, int studentId)
        {
            var month = new MonthFee()
            {
                Fees = fee,
                StudentId = studentId,
                IsPaid = false,
                Month = DateTime.Now,
                Name = name
            };
            await DbContext.MonthFees.AddAsync(month);
        }

    }
}
