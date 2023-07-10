using EL_Saher.Shared;
using EL_Saher.Shared.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;

        public decimal? Cost { get; set; }

        public int StudentsNumber { get; set; }


        public string Schedule { get; set; } = string.Empty;

        [InverseProperty("Course")]
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public  List<Exam>? Exams { get; set; } = null;
    }

    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string  ContactNumber { get; set; } = string.Empty;
        public string  StudentGrade { get; set; } = string.Empty;
        public int? StudentRate { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        
        public virtual ICollection<CourseAttendance> Attendances { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<MonthFee> MonthFees { get; set; }
	}


    public class Exam
    {
        [Key]
        public int ExamID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Score { get; set; }
        public decimal OutOf { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; } 
    }


    public class CourseAttendance
    {
        [Key]
        public int CourseAttendanceId { get; set; }
        public int SessionNumber { get; set; }
        public bool AttendanceStatus { get; set; } = false;
        public DateTime AttendanceDate { get; set; } = DateTime.Now;

        // relationship
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }

    public class MonthFee
    {
        [Key]
        public int MonthFeeId { get; set; }
        public string Name { get; set; } = String.Empty;
        public DateTime Month { get; set; } = DateTime.Now;
        public int Fees { get; set; } 
        public bool IsPaid { get; set; } = false;

        // relationship
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }


}
